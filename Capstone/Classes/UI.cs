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
                    string balanceToAdd = AskForString("\nPlease enter an amount in whole dollars, excluding decimals: ");

                    Console.WriteLine(vendoMatic.AddMoney(balanceToAdd));
                    menuOption = "-1";
                }
                else if (menuOption == "2")
                {
                    Console.WriteLine(vendoMatic.GetInventory() + "\n");
                    string key = AskForString("Please enter the item's key: ");
                    Console.WriteLine(vendoMatic.SelectProduct(key));
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

        private string AskForString(string message)
        {
            Console.Write(message);
            return Console.ReadLine();

        }


    }
}
