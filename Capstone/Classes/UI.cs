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
            FileFunction file = new FileFunction("vendingmachine.csv");
            Dictionary<string, Stack<Item>> vendingMachine = file.LoadUpFile();
        }
    }
}
