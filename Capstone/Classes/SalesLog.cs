using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Text;

namespace Capstone.Classes
{
    public static class SalesLog
    {
        public static void WriteLog(string text, decimal value, decimal balance)
        {
			try
			{
                using (StreamWriter sw = new StreamWriter("SalesLog.txt", true))
                {
                    sw.WriteLine($"{DateTime.UtcNow} {text} ${value:C2} ${balance:C2}");
                }
			}
			catch (Exception e)
			{

            }
        }
    }
}
