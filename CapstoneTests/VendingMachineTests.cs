using Capstone.Classes;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapstoneTests
{
    /* testing strings
       * test two+ happy paths
       * test edge cases
       * test null
       * test blank ""
       * test different CaSeS
       * test chars
       * test line breaks
       * test stray blank spaces included
       * test boundaries
       * test zero
       * test negative
       * test random numbers (not too large unless needed)
       * arrange
       * act
       * assert
       */
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
            string input1 = "5";
            string input2 = "10";
            string expected2 = "Your balance is now $15.00";

            string actual1 = sut.AddMoney(input1);
            string actual2 = sut.AddMoney(input2);

            Assert.AreEqual(expected2, actual2);
        }
        [TestMethod]
        public void AddMoneyNullOrEmpty() 
        {
            string input1 = null;
            string input2 = "";
            string expected1 = "An invalid input was passed.";
            string expected2 = "An invalid input was passed.";

            string actual1 = sut.AddMoney(input1);
            string actual2 = sut.AddMoney(input2);

            Assert.AreEqual(expected1, actual1);
            Assert.AreEqual(expected2, actual2);
        }
        [TestMethod]
        public void AddMoneyNegative()
        {
            string input1 = "-15";
            string input2 = "-1";
            string expected1 = "Please enter a value above zero. Please try again.";
            string expected2 = "Please enter a value above zero. Please try again.";

            string actual1 = sut.AddMoney(input1);
            string actual2 = sut.AddMoney(input2);

            Assert.AreEqual(expected1, actual1);
            Assert.AreEqual(expected2, actual2);
        }

        [TestMethod]
        public void SubtractMoneyHappyPathItemLessThanBalance() 
        {
            sut.AddMoney("15");
            decimal input1 = 5.0M;
            bool expected1 = true;

            bool actual1 = sut.SubtractMoney(input1);

            Assert.AreEqual(expected1, actual1);
        }
        [TestMethod]
        public void SubtractMoneyHappyPathItemMoreThanBalance() 
        {
            sut.AddMoney("15");
            decimal input1 = 25.0M;
            bool expected1 = false;

            bool actual1 = sut.SubtractMoney(input1);

            Assert.AreEqual(expected1, actual1);
        }
        [TestMethod]
        public void SubtractMoneyZeroBalance() 
        {
            decimal input1 = 25.0M;
            decimal input2 = 0M;
            decimal input3 = -1M;
            bool expected1 = false;

            bool actual1 = sut.SubtractMoney(input1);
            bool actual2 = sut.SubtractMoney(input2);
            bool actual3 = sut.SubtractMoney(input3);

            Assert.AreEqual(expected1, actual1);
            Assert.AreEqual(expected1, actual2);
            Assert.AreEqual(expected1, actual3);
        }
        [TestMethod]
        public void SubtractMoneyNegativeAndZeroPrice() 
        {
            sut.AddMoney("15");
            decimal input2 = -10M;
            decimal input3 = 0M;
            bool expected1 = false;


            bool actual2 = sut.SubtractMoney(input2);
            bool actual3 = sut.SubtractMoney(input3);


            Assert.AreEqual(expected1, actual2);
            Assert.AreEqual(expected1, actual3);

        }
        // this doesn't really call any parameters except the balance
        // balance is only a decimal and the values subtracted from it are divisible by 5
        // can't generate another test aside from happy path
        [TestMethod]
        public void GetChangeHappyPath() //Aseel
        {
            sut.AddMoney("1");
            string expected1 = "Your change due is $1.00. Dispensing 4 Quarter(s), 0 Dime(s), and 0 Nickel(s).";

            string actual1 = sut.GetChange();

            Assert.AreEqual(expected1, actual1);
        }
        // this doesn't really call any parameters except the file
        // can't generate another test aside from happy path
        [TestMethod]
        public void GetInventoryHappyPath() //Aseel
        {
            string expected1 = "Key   Price    Type   Brand                Quantity\nT1    $0.95    Chip   TestSnack            5\nT2    $1.95    Drink  TestBeverage         5\n";

            string actual1 = sut.GetInventory();

            Assert.AreEqual(expected1, actual1);
        }
        [TestMethod]
        public void CheckKeyHappyPath() //Aseel
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
        public void CheckKeyIsNullOrEmpty() //Aseel
        {
            string input1 = "";
            bool expected1 = false;
            string input2 = null;
            bool expected2 = false;
            string input3 = " ";
            bool expected3 = false;

            bool actual1 = sut.CheckKey(input1);
            bool actual2 = sut.CheckKey(input2);
            bool actual3 = sut.CheckKey(input3);

            Assert.AreEqual(expected1, actual1);
            Assert.AreEqual(expected2, actual2);
            Assert.AreEqual(expected3, actual3);
        }
        [TestMethod]
        public void CheckQuantityHappyPath() //Aseel
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
        public void CheckQuantityIsNullOrEmpty() //Aseel
        {
            string input1 = "";
            bool expected1 = false;
            string input2 = null;
            bool expected2 = false;
            string input3 = " ";
            bool expected3 = false;

            bool actual1 = sut.CheckQuantity(input1);
            bool actual2 = sut.CheckQuantity(input2);
            bool actual3 = sut.CheckQuantity(input3);

            Assert.AreEqual(expected1, actual1);
            Assert.AreEqual(expected2, actual2);
            Assert.AreEqual(expected3, actual3);
        }
        [TestMethod]
        public void CheckMoneyHappyPath() 
        {
            sut.AddMoney("1");
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
        public void CheckMoneyEmptyOrNegative() 
        {
            sut.AddMoney("1");
            string input1 = "T3";
            bool expected1 = false;
            string input2 = "T4";
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
            sut.AddMoney("10");
            string input1 = "T1";
            Item expected1 = new Item("TestSnack", "0.95", "Chip");

            Item actual1 = sut.Dispense(input1);

            CollectionAssert.Equals(expected1, actual1);
        }
        [TestMethod]
        public void DispenseZeroStock()
        {
            sut.AddMoney("10");
            sut.Dispense("T1");
            sut.Dispense("T1");
            sut.Dispense("T1");
            sut.Dispense("T1");
            sut.Dispense("T1");
            string input1 = "T1";
            Item expected1 = null;

            Item actual1 = sut.Dispense(input1);

            CollectionAssert.Equals(expected1, actual1);
        }
        [TestMethod]
        public void DispenseNotEnoughMoney()
        {
            sut.AddMoney("1");
         
            string input1 = "T2";
            Item expected1 = null;

            Item actual1 = sut.Dispense(input1);

            CollectionAssert.Equals(expected1, actual1);
        }
        [TestMethod]
        public void DispenseBadKey()
        {
            sut.AddMoney("1");

            string input1 = "wrdiufhiur";
            Item expected1 = null;

            Item actual1 = sut.Dispense(input1);

            CollectionAssert.Equals(expected1, actual1);
        }

        [TestMethod]
        public void SelectProductHappyPath() //Aseel
        {
            sut.AddMoney("10");
            string input1 = "T1";
            string input2 = "T2";
            string expected1 = "Dispensing TestSnack, for $0.95.\nCrunch Crunch, Yum!\n" +
                            "Your balance is now $9.05";
            string expected2 = "Dispensing TestBeverage, for $1.95.\nGlug Glug, Yum!\n" +
                            "Your balance is now $7.10";

            string actual1 = sut.SelectProduct(input1);
            string actual2 = sut.SelectProduct(input2);

            Assert.AreEqual(expected1, actual1);
            Assert.AreEqual(expected2, actual2);
        }
        [TestMethod]
        public void SelectProductIsNullOrEmpty() //Aseel
        {
            sut.AddMoney("10");
            string input1 = "";
            string input2 = null;
            string expected1 = "An invalid input was passed.";
            string expected2 = "An invalid input was passed.";

            string actual1 = sut.SelectProduct(input1);
            string actual2 = sut.SelectProduct(input2);

            Assert.AreEqual(expected1, actual1);
            Assert.AreEqual(expected2, actual2);
        }
        [TestMethod]
        public void SelectProductCase() //Aseel
        {
            sut.AddMoney("10");
            string input1 = "t1";
            string expected1 = "Dispensing TestSnack, for $0.95.\nCrunch Crunch, Yum!\n" +
                            "Your balance is now $9.05"; 

            string actual1 = sut.SelectProduct(input1); 

            Assert.AreEqual(expected1, actual1); 
        }
        [TestMethod]
        public void SelectProductTrim() //Aseel
        {
            sut.AddMoney("10");
            string input1 = "t1 ";
            string expected1 = "Dispensing TestSnack, for $0.95.\nCrunch Crunch, Yum!\n" +
                            "Your balance is now $9.05"; 

            string actual1 = sut.SelectProduct(input1);

            Assert.AreEqual(expected1, actual1);
        }
    }
}
