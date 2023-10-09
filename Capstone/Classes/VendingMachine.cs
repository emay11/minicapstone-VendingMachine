using Capstone.Exceptions;
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

            SalesLog.WriteLog("GIVE CHANGE:", startingBalance, 0.00M);
            Balance = 0;
            return $"Your change due is {startingBalance:C2}. Dispensing {(int)(numQuarters)} Quarter(s), {(int)(numDimes)} Dime(s), and {numNickels} Nickel(s).";
        }

        public string GetInventory()
        {
            string output = "Key   Price    Type   Brand                Quantity\n";
            //loop through every line in our dictionary and print to the console
            foreach (KeyValuePair<string, Stack<Item>> item in Inventory)
            {
                try
                {
                    Item currentStack = item.Value.Peek();
                    string price = currentStack.Price.ToString();
                    string name = currentStack.Name;
                    string type = currentStack.Type;
                    int count = item.Value.Count;
                    output += $"{item.Key,-6}${price,-8}{type,-7}{name,-20} {count.ToString()}\n";
                }
                catch (Exception e)
                {
                    output += $"{item.Key,-6}SOLD OUT\n";
                }
            }
            return output;
        }


        public bool CheckKey(string key)
        {
            return Inventory.ContainsKey(key);
        }

        public bool CheckQuantity(string key)
        {
            try
            {
                return Inventory[key].Count > 0;
            }
            catch (Exception)
            {
                return false;
            }
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
                return false;
            }

        }

        public Item Dispense(string key)
        {
            try
            {
                Item currentChoice = Inventory[key].Pop();
                SubtractMoney(currentChoice.Price);
                SalesLog.WriteLog($"{currentChoice.Name} {key}", currentChoice.Price, Balance);
                return currentChoice;
            }
            catch (SoldOutException soe)
            {
                return null;
            }
            catch (Exception)
            {
                return null;
            }

        }

        //exit program
        public void Exit()
        {
            Environment.Exit(0);
        }

    }
}
