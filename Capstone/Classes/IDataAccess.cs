using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public interface IDataAccess
    {
        Dictionary<string, Stack<Item>> LoadUpFile();
    }
}
