using Capstone.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml.Linq;

namespace Capstone.Classes
{
    public class FileFunction : IDataAccess
    {
        private const int Stock = 5;
        private string FileName { get; }
        public FileFunction(string fileName)
        {
            FileName = fileName;
        }
        private bool ValidateFile()
        {
            if (FileName.EndsWith(".csv"))
            {
                return File.Exists(FileName);
            }
            return false;
        }

        public Dictionary<string, Stack<Item>> LoadUpFile()
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
        private Dictionary<string, Stack<Item>> ExtractItem()
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

                        try
                        {
                            if (string.IsNullOrEmpty(line))
                            {
                                throw new InvalidFileException();
                            }

                            string[] splitLine = LineSplitter(line);
                            Item newItem = new Item(splitLine[1], splitLine[2], splitLine[3]);

                            Stack<Item> items = new Stack<Item>();
                            for (int i = 0; i < Stock; i++)
                            {
                                items.Push(newItem);
                            }
                            //add this all to the dictionary
                            output[splitLine[0].ToUpper()] = items;
                        }
                        catch (Exception e)
                        {
                            // if the line is bad, it'll throw an exception and go to the next line
                        }
                    }
                }
            }
            catch (Exception e)
            {

            }
            return output;

        }
        private string[] LineSplitter(string line)
        {
            // if this stays true through successive methods, then proceed
            if (PipeCounter(line) && ZeroPositionValidation(line) && SecondPositionValidation(line) && ThirdPositionValidation(line))
            {
                return line.Split("|");
            }
            return null;            
        }
        private bool PipeCounter(string line)
        {
            // check to make sure you have four elements
            // has to have a count of 3 pipes
            int pipeCounter = 0;
            for (int i = 0; i < line.Length; i++)
            {
                string character = line.Substring(i, 1);
                if (character == "|")
                {
                    pipeCounter++;
                }
            }
            if (pipeCounter == 3)
            {
                return true;
            }
            return false;
        }
        private bool ZeroPositionValidation(string line)
        {
            string[] splitLine = line.Split("|");
            //check the first value, the key, to make sure it's two characters
            int zeroPositionLength = splitLine[0].Length;

            if (zeroPositionLength == 2)
            {
                try
                {
                    // make sure the first value's second position is a number
                    string zeroPositionLastChar = splitLine[0].Substring(1);
                    // this will throw an exception if not valid
                    int zeroPositionLastCharAsInt = int.Parse(zeroPositionLastChar);
                    // if that is all valid
                    return true;

                }
                catch (BadStringException)
                {
                    return false;
                }
            }
            return false;
        }
        private bool SecondPositionValidation(string line)
        {
            string[] splitLine = line.Split("|");
            try
            {
                // make sure the second value can be converted to a decimal
                // if it can't, it'll throw an error
                decimal firstPositionAsDec = decimal.Parse(splitLine[1]);

                // if that is all valid
                return true;
            }
            catch (BadStringException)
            {
                return false;
            }
        }
        private bool ThirdPositionValidation(string line)
        {
            string[] splitLine = line.Split("|");

            // set up the type of snacks
            string[] snackType = new string[] { "Chip", "Candy", "Drink", "Gum" };

            if (snackType.Contains(splitLine[3]))
            {
                return true;
            }
            return false;

        }
    }
}
