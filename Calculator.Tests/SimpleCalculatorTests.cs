using CalculatorTest.Lib;
using CalculatorTest.Lib.Constants;
using Moq;
using NUnit.Framework;
using System;
using System.Linq.Expressions;

namespace Calculator.Tests
{
    public class SimpleCalculatorTests
    {
        Mock<IDiagnostics> diagnosticsMock = new Mock<IDiagnostics>();
        SimpleCalculator simpleCalc = null;
        string logResultFnOpParam = "";
        int logResultFnResultParam = 0;
        Expression<Action<IDiagnostics>> logResultFnExpr = x => x.LogResult(It.IsAny<string>(), It.IsAny<int>());

        [SetUp]
        public void Setup()
        {   
            diagnosticsMock.Setup(logResultFnExpr)
                            .Callback((string op, int result) => {
                                logResultFnOpParam = op;
                                logResultFnResultParam = result;
                            });
            simpleCalc = new SimpleCalculator(diagnosticsMock.Object);
        }

        private void Verify_Diagnostics_LogResult_Fn_Invocation(string expectedOperation, int expectedResult)
        {
            Assert.AreEqual(expectedOperation, logResultFnOpParam);
            Assert.AreEqual(expectedResult, logResultFnResultParam);
            diagnosticsMock.Verify(logResultFnExpr, Times.Once);
        }

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
            diagnosticsMock.Invocations.Clear();
            Assert.AreEqual(expectedResult, simpleCalc.Add(start, amount));
            Verify_Diagnostics_LogResult_Fn_Invocation(CalculatorOperationText.Add, expectedResult);
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
            diagnosticsMock.Invocations.Clear();
            Assert.AreEqual(expectedResult, simpleCalc.Subtract(start, amount));
            Verify_Diagnostics_LogResult_Fn_Invocation(CalculatorOperationText.Subtract, expectedResult);
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
            diagnosticsMock.Invocations.Clear();
            Assert.AreEqual(expectedResult, simpleCalc.Multiply(start, by));
            Verify_Diagnostics_LogResult_Fn_Invocation(CalculatorOperationText.Multiply, expectedResult);
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
            diagnosticsMock.Invocations.Clear();
            Assert.AreEqual(expectedResult, simpleCalc.Divide(start, by));
            Verify_Diagnostics_LogResult_Fn_Invocation(CalculatorOperationText.Divide, expectedResult);
        }

        [Test]
        [TestCase(0, 0)]
        [TestCase(10, 0)]
        public void Verify_Division_Operation_Divide_By_Zero(int start, int by)
        {
            var ex = Assert.Throws<CalculatorException>(() => simpleCalc.Divide(start, by));
            Assert.That(ex.Message == ExceptionErrorText.DivideByZeroException);
        }

        [Test]
        [TestCaseSource(nameof(OverflowExceptionUseCases))]
        public void Verify_Calculator_Operation_Overflow_Exception(int start, int by, string op)
        {
            var exMsg = string.Format(ExceptionErrorText.OverflowException, op);

            if (op == CalculatorOperationText.Add)
            {
                var ex = Assert.Throws<CalculatorException>(() => simpleCalc.Add(start, by));
                Assert.That(ex.Message == exMsg);
            }

            if (op == CalculatorOperationText.Subtract)
            {
                var ex = Assert.Throws<CalculatorException>(() => simpleCalc.Subtract(start, by));
                Assert.That(ex.Message == exMsg);
            }

            if (op == CalculatorOperationText.Multiply)
            {
                var ex = Assert.Throws<CalculatorException>(() => simpleCalc.Multiply(start, by));
                Assert.That(ex.Message == exMsg);
            }

            if (op == CalculatorOperationText.Divide)
            {
                var ex = Assert.Throws<CalculatorException>(() => simpleCalc.Divide(start, by));
                Assert.That(ex.Message == exMsg);
            }            
        }

        private static object[] OverflowExceptionUseCases =
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