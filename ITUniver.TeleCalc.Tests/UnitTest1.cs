using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ITUniver.TeleCalc.ConCalc;

namespace ITUniver.TeleCalc.Tests
{
    [TestClass]
    public class CalcTest
    {
        [TestMethod]
        public void TestSum()
        {
            // Arrange
            var calc = new Calc();
            var x = 1;
            var y = 2;

            // Act
            var result1 = calc.Sum(x, y);
            var result2 = calc.Sum(10, 0);

            // Assert
            Assert.AreEqual(3, result1);
            Assert.AreEqual(10, result2);
        }
    }
}
