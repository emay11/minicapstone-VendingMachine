using Capstone.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
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

        public string AddMoney(string value)
        {
            try
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new BadStringException();
                }
                int moneyToAdd = int.Parse(value.Trim());
                if (moneyToAdd > 0)
                {
                    Balance += moneyToAdd;
                    SalesLog.WriteLog("FEED MONEY:", moneyToAdd, Balance);
                    return $"Your balance is now {Balance:C2}";
                }
                else
                {
                    return "Please enter a value above zero. Please try again.";
                }
            }
            catch (BadStringException bse)
            {
                return bse.Message;
            }
            catch (Exception)
            {
                return "You've entered an incorrect value. Please try again.";
            }
        }

        public bool SubtractMoney(decimal value)
        {
            if ((Balance > 0) && (value > 0))
            {
                if (Balance >= value)
                {
                    Balance -= value;
                    return true;
                }
            }
            return false;
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
            try
            {
                return Inventory.ContainsKey(key);
            }
            catch (Exception)
            {
                return false;
            }
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
                if (currentChoice.Price > 0 && Balance >= currentChoice.Price)
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return false;
        }

        public Item Dispense(string key)
        {
            try
            {
                if (!string.IsNullOrEmpty(key) && CheckKey(key) && CheckQuantity(key) && CheckMoney(key))
                {
                    Item currentChoice = Inventory[key].Pop();
                    SubtractMoney(currentChoice.Price);

                    SalesLog.WriteLog($"{currentChoice.Name} {key}", currentChoice.Price, Balance);
                    return currentChoice;
                }
            }
            catch (SoldOutException soe)
            {
                return null;
            }
            catch (Exception)
            {
                return null;
            }
            return null;
        }

        public string SelectProduct(string key)
        {
            try
            {
                if (string.IsNullOrEmpty(key))
                {
                    throw new BadStringException();
                }
                string upperTrimmedKey = key.Trim().ToUpper();
                if (CheckKey(upperTrimmedKey))
                {
                    if (CheckQuantity(upperTrimmedKey))
                    {
                        if (CheckMoney(upperTrimmedKey))
                        {
                            Item canDispense = Dispense(upperTrimmedKey);
                            return $"Dispensing {canDispense.Name}, for {canDispense.Price:C2}.\n{canDispense.Sound}\n" +
                            $"Your balance is now {GetBalance():C2}";
                        }
                        else
                        {
                            return "Sorry, your balance is insufficient. Returning to the previous menu where you can add more money.";
                        }
                    }
                    else
                    {
                        return "Sorry, the item you want is unavailable. Please try again. Returning to the previous menu.";
                    }
                }
                else
                {
                    return "Sorry, you entered an invalid key. Please try again. Returning to the previous menu.";
                }
            }
            catch (BadStringException bse)
            {
                return bse.Message;
            }
        }

        //exit program
        public void Exit()
        {
            Environment.Exit(0);
        }

    }
}
