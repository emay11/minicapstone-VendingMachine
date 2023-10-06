using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Capstone.Classes
{
    public class VendingMachine
    {
        private Dictionary<string, Stack<Item>> Inventory { get; } = new Dictionary<string, Stack<Item>>();
        private decimal Balance { get; set; } = 0M;

        public VendingMachine(string fileName)
        {
            FileFunction inventory = new FileFunction(fileName);
            Inventory = inventory.LoadUpFile();
        }

        public decimal GetBalance()
        {
            return Balance;
        }

        public decimal AddMoney(decimal value)
        {
            Balance += value;
            SalesLog.WriteLog("FEED MONEY:", value, Balance);
            return Balance;
        }

        public bool SubtractMoney(decimal value)
        {
            if (Balance >= value)
            {
                Balance -= value;
                return true;
            }
            else
            {
                return false;
            }
        }

        public string GetChange()
        {

            const decimal Quarter = 0.25M;
            const decimal Dime = 0.10M;
            const decimal Nickel = 0.05M;
            decimal startingBalance = Balance;

            decimal numQuarters = Balance / Quarter;
            decimal calculatingChange = Balance % Quarter;

            decimal numDimes = calculatingChange / Dime;
            calculatingChange = calculatingChange % Dime;

            decimal numNickels = calculatingChange / Nickel;

            SalesLog.WriteLog("GIVE CHANGE:", Balance, 0.00M);
            Balance = 0;
            return $"Your change due is {startingBalance:C2}. Dispensing {numQuarters} Quarter(s), {numDimes} Dime(s), and {numNickels} Nickel(s).";
        }

        //create our menu
        public string GetInventory()
        {
            string output = "Key   Price   Type   Brand                Quantity\n";
            //loop through every line in our dictionary and print to the console
            foreach (KeyValuePair<string, Stack<Item>> item in Inventory)
            {
                Item currentStack = item.Value.Peek();
                string price = currentStack.Price.ToString();
                string name = currentStack.Name;
                string type = currentStack.Type;
                int count = item.Value.Count;
                string holdingCount = "";
                if (count == 0)
                {
                    holdingCount = "SOLD OUT";
                }
                else
                {
                    holdingCount = count.ToString();
                }
                output += $"{item.Key,-6}${price,-7}{type,-7}{name,-20} {holdingCount}\n";
            }
            return output;
        }

        public Item SelectProduct(string key)
        {
            //put in key 
            //check if key is valid 
            //if not return false 
            //      check quantity if zero, return false 
            //      check if have enough money 
            //          dispense product 
            //          subtract from quantity and subtract from balance 
            //          print item name, cost, sound, balance remaining 
            //          return true and add to Log
            //              return to purchase menu
            if (CheckKey(key) && CheckQuantity(key) && CheckMoney(key))
            {
                Item choice = Dispense(key);
                return choice;
            }
            else
            {
                return null;
            }
        }

        public bool CheckKey(string key)
        {
            return Inventory.ContainsKey(key);
        }

        public bool CheckQuantity(string key)
        {
            return Inventory[key].Count > 0;
        }

        public bool CheckMoney(string key)
        {
            try
            {
                Item currentChoice = Inventory[key].Peek();
                return Balance >= currentChoice.Price;
            }
            catch (Exception e)
            {
                Console.WriteLine("You've entered an incorrect value. Please try again.");
                return false;
            }

        }

        public Item Dispense(string key)
        {
            Item currentChoice = Inventory[key].Pop();
            SubtractMoney(currentChoice.Price);
            SalesLog.WriteLog($"{currentChoice.Name} {key}", currentChoice.Price, Balance);
            return currentChoice;
        }

        //exit program
        public void Exit()
        {
            Environment.Exit(0);
        }

    }
}
