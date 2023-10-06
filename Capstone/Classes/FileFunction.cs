using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Xml.Linq;

namespace Capstone.Classes
{
    public class FileFunction 
    {
        private const int Stock = 5;
        public string FileName { get; set; }
        public FileFunction (string fileName)
        {
            FileName = fileName;
        }
        private Dictionary<string,Stack<Item>> ExtractItem()
        {

            //validate file existence
            //open the file
            //go line by line
            //make an item object
            //add those objects to a stack
            //add the stack to the dictionary
            Dictionary<string, Stack<Item>> output = new Dictionary<string, Stack<Item>>();

            try
            {
                using (StreamReader sr = new StreamReader(FileName))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        string[] splitLine = line.Split("|");
                        
                        try
                        {
                            Item newItem = new Item(splitLine[1], splitLine[2], splitLine[3]);
                            Stack<Item> items = new Stack<Item>();
                            for (int i = 0; i < Stock; i++)
                            {
                                items.Push(newItem);
                            }
                            //add this all to the dictionary
                            output[splitLine[0]] = items;
                        }
                        catch (Exception e)
                        {

                            //TODO create error log
                            //problem creating object
                        }
                    }
                }
            }
            catch (Exception e)
            {
                //TODO create error log
                //file did'nt load
                
            }
            return output;

        }

        private bool ValidateFile()
        {
            return File.Exists(FileName);
        }

        public Dictionary<string, Stack<Item>>LoadUpFile()
        {
            if (ValidateFile())
            {
                return ExtractItem();
            }
            else
            {
                return null;
            }
        }

    }
}
