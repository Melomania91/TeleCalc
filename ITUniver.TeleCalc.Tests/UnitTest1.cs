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

        [TestMethod]
        public void TestSubtraction()
        {
            // Arrange
            var calc = new Calc();
            var x = 5;
            var y = 3;

            // Act
            var result1 = calc.Subtraction(x, y);
            var result2 = calc.Subtraction(7, 2);

            // Assert
            Assert.AreEqual(2, result1);
            Assert.AreEqual(5, result2);
        }

        [TestMethod]
        public void TestDivision()
        {
            // Arrange
            var calc = new Calc();
            var x = 6;
            var y = 3;

            // Act
            var result1 = calc.Division(x, y);
            var result2 = calc.Division(x, 1);
            var result3 = calc.Division(x, 0);

            // Assert
            Assert.AreEqual(2, result1);
            Assert.AreEqual(x, result2);
            Assert.AreEqual(null, result3);
        }

        [TestMethod]
        public void TestMultipl()
        {
            // Arrange
            var calc = new Calc();
            var x = 2;
            var y = 3;

            // Act
            var result1 = calc.Multiplication(x, y);
            var result2 = calc.Multiplication(x, 1);
            var result3 = calc.Multiplication(x, 0);

            // Assert
            Assert.AreEqual(6, result1);
            Assert.AreEqual(x, result2);
            Assert.AreEqual(0, result3);
        }
    }
}
