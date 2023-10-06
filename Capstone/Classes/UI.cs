using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class UI
    {
        public void Run()
        {
            //here we'll do the console writelines and readlines
            VendingMachine vendoMatic = new VendingMachine("vendingmachine.csv");
            //Dictionary<string, Stack<Item>> vendingMachine = file.LoadUpFile();
            vendoMatic.GetInventory();
        }
    }
}
