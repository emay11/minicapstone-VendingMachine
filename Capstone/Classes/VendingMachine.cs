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
            SalesLog.WriteLog("FEED MONEY:",value, Balance);
            return Balance;
        }
        
        public bool SubtractMoney(decimal value)
        {
            if (Balance >= value)
            {
                Balance -= value;
                Console.WriteLine("Your balance is now $" + Balance);
                return true;
            }
            else
            {
                Console.WriteLine("You do not have sufficient funds, please enter more money");
                return false;
            }
        }

        public bool GetChange()
        {
           
            const decimal Quarter = 0.25M;
            const decimal Dime = 0.10M;
            const decimal Nickel = 0.05M;

            decimal numQuarters = Balance / Quarter;
            decimal calculatingChange = Balance % Quarter;

            decimal numDimes = calculatingChange / Dime;
            calculatingChange = calculatingChange % Dime;

            decimal numNickels = calculatingChange / Nickel;

            Console.WriteLine($"Your change due is ${Balance}. Dispensing {numQuarters} Quarter(s), {numDimes} Dime(s), {numNickels} Nickel(s).");
            SalesLog.WriteLog("GIVE CHANGE:", Balance, 0.00M);
            Balance = 0;
            return true;
        }

        //create our menu
        public bool GetInventory()
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
            Console.WriteLine(output);
            return true;
        }

        public bool SelectProduct(string key)
        {
            //put in key y
            //check if key is valid y
            //if not return false y
            //      check quantity if zero, return false y
            //      check if have enough money y
            //          dispense product y
            //          subtract from quantity and subtract from balance y
            //          print item name, cost, sound, balance remaining y
            //          return true and add to Log 
            //              return to purchase menu
            if (CheckKey(key) && CheckQuantity(key) && CheckMoney(key))
            {
                Dispense(key);
                return true;
            }
            else
            {
                return false;
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
            Item currentChoice = Inventory[key].Peek();
            return Balance >= currentChoice.Price;
        }

        public bool Dispense(string key)
        {
            Item currentChoice = Inventory[key].Pop();
            SubtractMoney(currentChoice.Price);
            Console.WriteLine($"Dispensing {currentChoice.Name}, for ${currentChoice.Price}.\n{currentChoice.Sound}\n");
            SalesLog.WriteLog($"{currentChoice.Name} {key}", currentChoice.Price, Balance);
            return true;
        }

        //exit program
        public void Exit()
        {
            Environment.Exit(0);
        }

    }
}
