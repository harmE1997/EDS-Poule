using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EDS_Poule;
namespace EDSTests
{
    [TestClass]
    public class EstimationTests
    {

        [TestMethod]
        public void TestConstructorWithCorrectInput()
        {
            Estimations es = new Estimations(50,20);
            Assert.AreEqual(2, es.Answers.Count);
            Assert.AreEqual(50, es.Answers[EstimationKeys.Reds].Answer);
            Assert.AreEqual(20, es.Answers[EstimationKeys.Goals].Answer);
            Assert.AreEqual(20, es.Answers[EstimationKeys.Reds].Max);
            Assert.AreEqual(10, es.Answers[EstimationKeys.Goals].Max);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestConstructorWithInvalidFirstArgument()
        {
            Estimations e = new Estimations(-1, 0);
        }

        [TestMethod]
        public void TestCheckWithCorrectInput()
        {
            Estimations es = new Estimations(50, 20);
            Estimations hoes = new Estimations(55, 22);
            Assert.AreEqual(23, es.CheckEstimations(hoes, 34));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCheckWithInvalidInput()
        {
            Estimations es = new Estimations(50, 20);
            es.CheckEstimations(null, 34);
        }
    }
}
