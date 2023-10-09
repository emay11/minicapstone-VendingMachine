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
            Console.WriteLine("Welcome to the Vendo-Matic 800 Vending Machine");
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
        //AT: I looked this up online but I am not sure if i can get it to work
        //the menus have the same basic structure so it should be possible to refactor them out
        //although I am not sure how to handle the exit function, since it is different to the purchase sub menu's option of console.write
        //private string Menu(string message, Func<string> menuOptionOne, Func<string> menuOptionTwo, Func<string> menuOptionThree) { }
        

        private string MainMenu()
        {
            string menuOption = "-1";
            do
            {
                menuOption = AskFor123("\n[1] Display vending machine items\n[2] Purchase\n[3] Exit\n\nPlease enter a menu option: ");
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
                else
                {
                    Console.WriteLine("Please enter either 1, 2, or 3.");
                }

            } while (menuOption == "-1");
            return "";
        }

        private string PurchaseSubMenu()
        {
            string menuOption = "-1";
            do
            {
                menuOption = AskFor123($"\nCurrent balance available: {vendoMatic.GetBalance():C2}\n\n[1] Feed money\n[2] Select product\n[3] Finish transaction\n\nPlease enter a menu option: ");
                if (menuOption == "1")
                {
                    string balanceToAdd = AskForInteger("\nPlease enter an amount in whole dollars, excluding decimals: ");                     
                    
                    Console.WriteLine(vendoMatic.AddMoney(balanceToAdd));
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
                else
                {
                    Console.WriteLine("Please enter either 1, 2, or 3.");
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
            string number = Console.ReadLine().Trim();
            if (number == "1" || number == "2" || number == "3")
            {
                return number;
            }
            else
            {
                return "-1";
            }

        }

        private string AskForInteger(string message)
        {
            Console.Write(message);
            return Console.ReadLine();

        }


    }
}
