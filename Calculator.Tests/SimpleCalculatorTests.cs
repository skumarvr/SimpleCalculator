using CalculatorTest.Lib;
using CalculatorTest.Lib.Constants;
using NUnit.Framework;
using System;

namespace Calculator.Tests
{
    public class SimpleCalculatorTests
    {
        [Test]
        [TestCase(10, 10, 20)]
        [TestCase(0, 0, 0)]
        [TestCase(10, 0, 10)]
        [TestCase(10, -5, 5)]
        [TestCase(10, -10, 0)]
        [TestCase(10, -15, -5)]
        [TestCase(0, 10, 10)]
        [TestCase(-5, 10, 5)]
        [TestCase(-10, 10, 0)]
        [TestCase(-15, 10, -5)]
        [TestCase(-15, -10, -25)]
        public void Verify_Add_Operation_With_Valid_Inputs(int start, int amount, int expectedResult)
        {
            var sc = new SimpleCalculator();
            Assert.AreEqual(expectedResult, sc.Add(start, amount));
        }

        [Test]
        [TestCase(10, 10, 0)]
        [TestCase(0, 0, 0)]
        [TestCase(10, 0, 10)]
        [TestCase(10, -5, 15)]
        [TestCase(10, -10, 20)]
        [TestCase(10, -15, 25)]
        [TestCase(0, 10, -10)]
        [TestCase(-5, 10, -15)]
        [TestCase(-10, 10, -20)]
        [TestCase(-15, 10, -25)]
        [TestCase(-15, -10, -5)]
        public void Verify_Subtract_Operation_With_Valid_Inputs(int start, int amount, int expectedResult)
        {
            var sc = new SimpleCalculator();
            Assert.AreEqual(expectedResult, sc.Subtract(start, amount));
        }

        [Test]
        [TestCase(10, 10, 100)]
        [TestCase(0, 0, 0)]
        [TestCase(10, 0, 0)]
        [TestCase(10, -5, -50)]
        [TestCase(0, 10, 0)]
        [TestCase(-5, 10, -50)]
        [TestCase(-10, -10, 100)]
        public void Verify_Multiply_Operation_With_Valid_Inputs(int start, int by, int expectedResult)
        {
            var sc = new SimpleCalculator();
            Assert.AreEqual(expectedResult, sc.Multiply(start, by));
        }

        [Test]
        [TestCase(10, 10, 1)]
        [TestCase(10, 5, 2)]
        [TestCase(10, 3, 3)]   // 3.333
        [TestCase(11, 3, 3)]   // 3.666
        [TestCase(10, 4, 2)]   // 2.5
        [TestCase(10, -5, -2)]
        [TestCase(-10, 5, -2)]
        [TestCase(-10, -5, 2)]
        [TestCase(0, 10, 0)]
        [TestCase(5, 10, 0)]   // 0.25
        [TestCase(5, -10, 0)]  // -0.25
        [TestCase(-5, 10, 0)]  // -0.25
        public void Verify_Division_Operation_With_Valid_Inputs(int start, int by, int expectedResult)
        {
            var sc = new SimpleCalculator();
            Assert.AreEqual(expectedResult, sc.Divide(start, by));
        }

        [Test]
        [TestCase(0, 0)]
        [TestCase(10, 0)]
        public void Verify_Division_Operation_Divide_By_Zero(int start, int by)
        {
            var sc = new SimpleCalculator();
            var ex = Assert.Throws<CalculatorException>(() => sc.Divide(start, by));

            Assert.That(ex.Message == ExceptionErrorText.DivideByZeroException);
        }

        [Test]
        [TestCaseSource(nameof(OverflowExceptionUseCases))]
        public void Verify_Calculator_Operation_Overflow_Exception(int start, int by, string op)
        {
            var exMsg = string.Format(ExceptionErrorText.OverflowException, op);
            var sc = new SimpleCalculator();

            if (op == CalculatorOperationText.Add)
            {
                var ex = Assert.Throws<CalculatorException>(() => sc.Add(start, by));
                Assert.That(ex.Message == exMsg);
            }

            if (op == CalculatorOperationText.Subtract)
            {
                var ex = Assert.Throws<CalculatorException>(() => sc.Subtract(start, by));
                Assert.That(ex.Message == exMsg);
            }

            if (op == CalculatorOperationText.Multiply)
            {
                var ex = Assert.Throws<CalculatorException>(() => sc.Multiply(start, by));
                Assert.That(ex.Message == exMsg);
            }

            if (op == CalculatorOperationText.Divide)
            {
                var ex = Assert.Throws<CalculatorException>(() => sc.Divide(start, by));
                Assert.That(ex.Message == exMsg);
            }            
        }

        static object[] OverflowExceptionUseCases =
        {
            new object[] { int.MaxValue, 1, CalculatorOperationText.Add },
            new object[] { int.MinValue, -1, CalculatorOperationText.Add },
            new object[] { int.MaxValue, -1, CalculatorOperationText.Subtract },
            new object[] { int.MinValue, 1, CalculatorOperationText.Subtract },
            new object[] { int.MaxValue, 2, CalculatorOperationText.Multiply },
            new object[] { int.MinValue, 2, CalculatorOperationText.Multiply },
            new object[] { int.MinValue, -1, CalculatorOperationText.Divide }
        };
    }
}