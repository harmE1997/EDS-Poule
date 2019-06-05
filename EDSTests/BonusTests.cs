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
            BonusQuestions q = new BonusQuestions("a", new string[]{"p", "q" }, "c", "d", "e", "f", new string[]{"r", "s" }, new string[]{ "h", "i" },"j", "k", "l", "m",
                 new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 });
            Assert.AreEqual("a", q.Answers[BonusKeys.Kampioen].Answer);
            Assert.IsFalse(q.Answers[BonusKeys.Kampioen].IsArray);
            Assert.AreEqual(25, q.Answers[BonusKeys.Kampioen].Points);

            Assert.AreEqual("p", q.Answers[BonusKeys.Degradanten].AnswerArray[0]);
            Assert.AreEqual("q", q.Answers[BonusKeys.Degradanten].AnswerArray[1]);
            Assert.IsTrue(q.Answers[BonusKeys.Degradanten].IsArray);
            Assert.AreEqual(10, q.Answers[BonusKeys.Degradanten].Points);

            Assert.AreEqual("c", q.Answers[BonusKeys.Topscorer].Answer);
            Assert.IsFalse(q.Answers[BonusKeys.Topscorer].IsArray);
            Assert.AreEqual(20, q.Answers[BonusKeys.Topscorer].Points);

            Assert.AreEqual("d", q.Answers[BonusKeys.Trainer].Answer);
            Assert.IsFalse(q.Answers[BonusKeys.Trainer].IsArray);
            Assert.AreEqual(20, q.Answers[BonusKeys.Trainer].Points);

            Assert.AreEqual("e", q.Answers[BonusKeys.Winterkampioen].Answer);
            Assert.IsFalse(q.Answers[BonusKeys.Winterkampioen].IsArray);
            Assert.AreEqual(15, q.Answers[BonusKeys.Winterkampioen].Points);

            Assert.AreEqual("f", q.Answers[BonusKeys.Ronde].Answer);
            Assert.IsFalse(q.Answers[BonusKeys.Ronde].IsArray);
            Assert.AreEqual(10, q.Answers[BonusKeys.Ronde].Points);

            Assert.AreEqual("r", q.Answers[BonusKeys.Promovendi].AnswerArray[0]);
            Assert.AreEqual("s", q.Answers[BonusKeys.Promovendi].AnswerArray[1]);
            Assert.IsTrue(q.Answers[BonusKeys.Promovendi].IsArray);
            Assert.AreEqual(10, q.Answers[BonusKeys.Promovendi].Points);

            Assert.AreEqual("h", q.Answers[BonusKeys.Finalisten].AnswerArray[0]);
            Assert.AreEqual("i", q.Answers[BonusKeys.Finalisten].AnswerArray[1]);
            Assert.IsTrue(q.Answers[BonusKeys.Finalisten].IsArray);
            Assert.AreEqual(10, q.Answers[BonusKeys.Finalisten].Points);
        }
    }
}
