using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Capstone.Classes
{
    public class UI
    {
        private const string InventoryLoad = "vendingmachine.csv";
        VendingMachine vendoMatic = new VendingMachine(InventoryLoad);
        public void Run()
        {
            Console.WriteLine("Welcome to the Vendo-Matic 800 Vending Machine\n");
            //here we'll do the console writelines and readlines
            //Load our vending machine
            //load main menu
            //get a response
            //validate the response
            //if error, loop
            //based on response do an action
            //if [2] show purchase sub-menu
            //validate response
            // if [1] add money
            //valide integer entry
            //if error, loop
            //if valid, show purchase sub-menu
            //if [2] get the key
            //validate key entry
            //if error, loop
            //dispense product break loop
            //if [3] dispense change
            //break loop to main menu
            MainMenu();
        }

        private string MainMenu()
        {
            string menuOption = "-1";
            do
            {
                menuOption = AskFor123("[1] Display vending machine items\n[2] Purchase\n[3] Exit\n\nPlease enter a menu option: ");
                if (menuOption == "1")
                {
                    Console.WriteLine(vendoMatic.GetInventory());
                    menuOption = "-1";
                }
                else if (menuOption == "2")
                {
                    PurchaseSubMenu();
                    menuOption = "-1";
                }
                else if (menuOption == "3")
                {
                    vendoMatic.Exit();
                }

            } while (menuOption == "-1");
            return "";
        }

        private string PurchaseSubMenu()
        {
            string menuOption = "-1";
            do
            {
                menuOption = AskFor123($"Current money provided: {vendoMatic.GetBalance():C2}\n\n[1] Feed money\n[2] Select product\n[3] Finish transaction\n\nPlease enter a menu option: ");
                if (menuOption == "1")
                {
                    int balanceToAdd = 0;
                    do
                    {
                        balanceToAdd = AskForInteger("Please enter an amount in whole dollars, excluding decimals: ");
                    } while (balanceToAdd <= 0);
                    vendoMatic.AddMoney(balanceToAdd);
                    Console.WriteLine($"Your balance is now {vendoMatic.GetBalance():C2}");
                    menuOption = "-1";
                }
                else if (menuOption == "2")
                {
                    ProductSelection();
                    menuOption = "-1";
                }
                else if (menuOption == "3")
                {
                    Console.WriteLine(vendoMatic.GetChange());
                }
            } while (menuOption == "-1");
            return "";
        }

        private string ProductSelection()
        {
            bool keepGoing = false;
            do
            {
                Console.WriteLine(vendoMatic.GetInventory() + "\n");
                Console.Write("Please enter the item's key: ");
                string key = Console.ReadLine().Trim().ToUpper();
                if (vendoMatic.CheckKey(key))
                {
                    if (vendoMatic.CheckQuantity(key))
                    {
                        if (vendoMatic.CheckMoney(key))
                        {
                            Item canDispense = vendoMatic.Dispense(key);
                            Console.WriteLine($"Dispensing {canDispense.Name}, for {canDispense.Price:C2}.\n{canDispense.Sound}\n");
                            Console.WriteLine($"Your balance is now {vendoMatic.GetBalance():C2}");
                        }
                        else
                        {
                            Console.WriteLine("Sorry, your balance is insufficient. Returning to the previous menu where you can add more money.");

                        }                        
                    }
                    else
                    {
                        Console.WriteLine("Sorry, the item you want is unavailable. Please try again. Returning to the previous menu.");
                    }
                }
                else
                {
                    Console.WriteLine("Sorry, you entered an invalid key. Please try again. Returning to the previous menu.");
                }
            } while (keepGoing);
            return "";
        }

        private string AskFor123(string message)
        {
            Console.Write(message);
            string number = Console.ReadLine();
            if (number == "1" || number == "2" || number == "3")
            {
                return number;
            }
            else
            {
                return "-1";
            }

        }

        private int AskForInteger(string message)
        {
            Console.Write(message);
            try
            {
                return int.Parse(Console.ReadLine());
            }
            catch (Exception e)
            {
                Console.WriteLine("You entered an incorrect value. Please try again.");
                return -1;
            }

        }


    }
}
