using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class UI
    {
        private const string InventoryLoad = "vendingmachine.csv";
        public void Run()
        {
            //here we'll do the console writelines and readlines

            //Load our vending machine
            VendingMachine vendoMatic = new VendingMachine(InventoryLoad);
            vendoMatic.GetInventory();
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
            string menuOption = "-1";
            do
            {
                menuOption = AskFor123("[1] Display Vending Machine Items\n[2] Purchase\n[3] Exit");

            } while (menuOption != "-1");
        }

        public string AskFor123(string message)
        {
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


    }
}
