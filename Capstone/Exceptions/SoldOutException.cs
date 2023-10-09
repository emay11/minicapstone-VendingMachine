using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Exceptions
{
    public class SoldOutException : Exception
    {
        public SoldOutException() : base("This item is sold out.")
        {
        }
    }
}
