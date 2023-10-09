using Capstone.Classes;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapstoneTests
{
    [TestClass]
    public class VendingMachineTests
    {
        VendingMachine sut = new VendingMachine("Test File.csv");
        [TestInitialize]
        public void init()
        {
            VendingMachine sut = new VendingMachine("Test File.csv");             
        }

        [TestMethod]
        public void GetBalanceHappyPath()
        {
            decimal actual = sut.GetBalance();
            Assert.AreEqual(0.00M, actual);
        }
        [TestMethod] 
        public void AddMoneyHappyPath()
        {
            decimal input1 = 5.0M;
            decimal input2 = 10.0M;
            decimal expected2 = 15.0M;
            
            sut.AddMoney(input1);
            sut.AddMoney(input2);

            Assert.AreEqual(expected2, sut.GetBalance()); 
        }
        [TestMethod] 
        public void SubtractMoneyHappyPathItemLessThanBalance()
        {
            sut.AddMoney(15M);
            decimal input1 = 5.0M; 
            bool expected1 = true;

            bool actual1 = sut.SubtractMoney(input1); 

            Assert.AreEqual(expected1, actual1);
        }
        [TestMethod]
        public void SubtractMoneyHappyPathItemMoreThanBalance()
        {
            sut.AddMoney(15M);
            decimal input1 = 25.0M; 
            bool expected1 = false;

            bool actual1 = sut.SubtractMoney(input1); 

            Assert.AreEqual(expected1, actual1);
        }
        [TestMethod] 
        public void GetChangeHappyPath()
        {
            sut.AddMoney(0.65M);
            string expected1 = "Your change due is $0.65. Dispensing 2 Quarter(s), 1 Dime(s), and 1 Nickel(s).";

            string actual1 = sut.GetChange();
            
            Assert.AreEqual(expected1, actual1);
        }
        [TestMethod]
        public void GetInventoryHappyPath()
        { 
            string expected1 = "Key   Price    Type   Brand                Quantity\nT1    $0.95    Chip   TestSnack            5\nT2    $1.95    Drink  TestBeverage         5\n";

            string actual1 = sut.GetInventory();

            Assert.AreEqual(expected1, actual1);
        }
        [TestMethod]
        public void CheckKeyHappyPath()
        {
            string input1 = "T1";
            bool expected1 = true;
            string input2 = "X1";
            bool expected2 = false;

            bool actual1 = sut.CheckKey(input1);
            bool actual2 = sut.CheckKey(input2);

            Assert.AreEqual(expected1, actual1);
            Assert.AreEqual(expected2, actual2);
        }
        [TestMethod]
        public void CheckQuantityHappyPath()
        {
            string input1 = "T1";
            bool expected1 = true;
            string input2 = "X1";
            bool expected2 = false;

            bool actual1 = sut.CheckQuantity(input1);
            bool actual2 = sut.CheckQuantity(input2);

            Assert.AreEqual(expected1, actual1);
            Assert.AreEqual(expected2, actual2);
        }
        [TestMethod]
        public void CheckMoneyHappyPath()
        {
            sut.AddMoney(1M);
            string input1 = "T1";
            bool expected1 = true;
            string input2 = "T2";
            bool expected2 = false;

            bool actual1 = sut.CheckMoney(input1);
            bool actual2 = sut.CheckMoney(input2);

            Assert.AreEqual(expected1, actual1);
            Assert.AreEqual(expected2, actual2);
        }
        [TestMethod]
        //TODO check this test method makes sense
        // don't think it should be equals
        public void DispenseHappyPath()
        {
            sut.AddMoney(10M);
            string input1 = "T1";
            Item expected1 = new Item("TestSnack", "0.95", "Chip"); 

            Item actual1 = sut.Dispense(input1);

            CollectionAssert.Equals(expected1, actual1);
        }
    }
}
