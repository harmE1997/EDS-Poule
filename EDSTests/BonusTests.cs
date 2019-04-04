using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EDS_Poule;
using Microsoft.CSharp.RuntimeBinder;

namespace EDSTests
{
    /// <summary>
    /// Summary description for BonusTests
    /// </summary>
    [TestClass]
    public class BonusTests
    {
        public BonusTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestConstructor()
        {
            BonusQuestions q = new BonusQuestions("a", "b", "c", "d", "e", "f", "g", new string[] { "h", "i" });
            Assert.AreEqual("a", q.Answers["Kampioen"].Answer);
            Assert.IsFalse(q.Answers["Kampioen"].IsArray);
            Assert.AreEqual(25, q.Answers["Kampioen"].Points);

            Assert.AreEqual("b", q.Answers["Degradant"].Answer);
            Assert.IsFalse(q.Answers["Degradant"].IsArray);
            Assert.AreEqual(20, q.Answers["Degradant"].Points);

            Assert.AreEqual("c", q.Answers["Topscorer"].Answer);
            Assert.IsFalse(q.Answers["Topscorer"].IsArray);
            Assert.AreEqual(20, q.Answers["Topscorer"].Points);

            Assert.AreEqual("d", q.Answers["Trainer"].Answer);
            Assert.IsFalse(q.Answers["Trainer"].IsArray);
            Assert.AreEqual(20, q.Answers["Trainer"].Points);

            Assert.AreEqual("e", q.Answers["Winterkampioen"].Answer);
            Assert.IsFalse(q.Answers["Winterkampioen"].IsArray);
            Assert.AreEqual(15, q.Answers["Winterkampioen"].Points);

            Assert.AreEqual("f", q.Answers["Championround"].Answer);
            Assert.IsFalse(q.Answers["Championround"].IsArray);
            Assert.AreEqual(10, q.Answers["Championround"].Points);

            Assert.AreEqual("g", q.Answers["Kampioendivisie1"].Answer);
            Assert.IsFalse(q.Answers["Kampioendivisie1"].IsArray);
            Assert.AreEqual(20, q.Answers["Kampioendivisie1"].Points);

            Assert.AreEqual("h", q.Answers["Finalisten"].Answer[0]);
            Assert.AreEqual("i", q.Answers["Finalisten"].Answer[1]);
            Assert.IsTrue(q.Answers["Finalisten"].IsArray);
            Assert.AreEqual(10, q.Answers["Finalisten"].Points);
        }
    }
}
