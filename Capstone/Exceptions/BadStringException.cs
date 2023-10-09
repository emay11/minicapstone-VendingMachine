using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Exceptions
{
    public class BadStringException : Exception
    {
        public BadStringException() : base("An invalid input was passed.")
        {
        }
    }
}
