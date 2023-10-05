using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class Item
    {
        //talked about making objects for each type but decided not to
        public string Name { get; }
        public decimal Price { get; }
        public string Sound
        {
            get
            {
                return GetSound[Type]; 
            }
            
        }
        public string Type { get; }


        public Item(string name, string price, string type)
        {
            Name = name;
            Price = decimal.Parse(price);
            Type = type;
        }

        private Dictionary<string, string> GetSound = new Dictionary<string, string>()
        {
            {"Chip", "Crunch Crunch, Yum!" },
            {"Candy", "Munch Munch, Yum!" },
            {"Drink", "Glug Glug, Yum!" },
            {"Gum", "Chew Chew, Yum!" }
        };
        public override string ToString()
        {
            return Type + ", " + Name;
        }
    }
}
