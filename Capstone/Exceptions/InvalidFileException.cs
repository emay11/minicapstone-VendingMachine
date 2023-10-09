using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Exceptions
{
    public class InvalidFileException : Exception
    {
        public InvalidFileException() : base("There was a problem reading the file.")
        {
        }
    }
}
