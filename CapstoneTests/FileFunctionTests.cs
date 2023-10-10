using Capstone.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapstoneTests
{
    [TestClass]
    public class FileFunctionTests
    {
        
        [TestMethod]
        public void LoadUpFileHappyPath()
        {
            // build the expected value in full
            FileFunction sut = new FileFunction("Test File.csv");
            Dictionary<string, Stack<Item>> expected = new Dictionary<string, Stack<Item>>();
            Item item1 = new Item("TestSnack", "0.95", "Chip");
            Item item2 = new Item("TestBeverage", "1.95", "Drink");
            Stack<Item> stack1 = new Stack<Item>();
            for(int i=0; i < 5; i++)
            {
                stack1.Push(item1);
            }
            Stack<Item> stack2 = new Stack<Item>();
            for (int i = 0; i < 5; i++)
            {
                stack2.Push(item2);
            }
            // add the stacks to the list
            expected["T1"] = stack1;
            expected["T2"] = stack2;

            // load dictionary
            // pull out the stack, convert to list
            Dictionary<string, Stack<Item>> actual = sut.LoadUpFile();

            // pull the count and properties for the expected values
            int expected1Count = stack1.Count;
            int expected2Count = stack2.Count;
            string expected1Name = expected["T1"].Peek().Name;
            decimal expected1Price = expected["T1"].Peek().Price;
            string expected1Sound = expected["T1"].Peek().Sound;
            string expected1Type = expected["T1"].Peek().Type;
            string expected2Name = expected["T1"].Peek().Name;
            decimal expected2Price = expected["T1"].Peek().Price;
            string expected2Sound = expected["T1"].Peek().Sound;
            string expected2Type = expected["T1"].Peek().Type;

            // pull the count and properties for the actual values
            int actual1Count = actual["T1"].Count;
            int actual2Count = actual["T2"].Count;
            string actual1Name = actual["T1"].Peek().Name;
            decimal actual1Price = actual["T1"].Peek().Price;
            string actual1Sound = actual["T1"].Peek().Sound;
            string actual1Type = actual["T1"].Peek().Type;
            string actual2Name = actual["T1"].Peek().Name;
            decimal actual2Price = actual["T1"].Peek().Price;
            string actual2Sound = actual["T1"].Peek().Sound;
            string actual2Type = actual["T1"].Peek().Type;

            //Assert 
            // for each component
            Assert.AreEqual(expected1Count, actual1Count);
            Assert.AreEqual(expected1Name, actual1Name);
            Assert.AreEqual(expected1Price, actual1Price);
            Assert.AreEqual(expected1Sound, actual1Sound);
            Assert.AreEqual(expected1Type, actual1Type);
            Assert.AreEqual(expected2Count, actual2Count);
            Assert.AreEqual(expected2Name, actual2Name);
            Assert.AreEqual(expected2Price, actual2Price);
            Assert.AreEqual(expected2Sound, actual2Sound);
            Assert.AreEqual(expected2Type, actual2Type);

        }
        [TestMethod]
        public void LoadUpFileNullAndEmptyFile()
        {
            FileFunction sut = new FileFunction("empty.csv");
            string expected = null;

            Dictionary<string, Stack<Item>> actual = sut.LoadUpFile();

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void LoadUpFileDoesntEndWithCSV()
        {
            FileFunction sut = new FileFunction("empty.txt");
            string expected = null;

            Dictionary<string, Stack<Item>> actual = sut.LoadUpFile();

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void LoadUpFileJunkyData()
        {
            // has bad key
            // has too few elements
            // has too many elements
            // doesn't have decimals
            // has negative decimals
            // has incorrect type
            FileFunction sut = new FileFunction("Junky Test File.csv");
            Dictionary<string, Stack<Item>> expected = new Dictionary<string, Stack<Item>>();

            Dictionary<string, Stack<Item>> actual = sut.LoadUpFile();

            int expectedKeyCount = 2;
            int actualKeyCount = 0;
            
            foreach(KeyValuePair<string, Stack<Item>> key in actual)
            {
                actualKeyCount++;
            }
            Assert.AreEqual(expectedKeyCount, actualKeyCount);
        }
    }
}
