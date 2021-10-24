using CalculatorTest.Lib.Constants;
using System;

namespace CalculatorTest.Lib
{
    public class SimpleCalculator : ISimpleCalculator
    {
        private IDiagnostics _diagnostics = null;

        public SimpleCalculator(IDiagnostics diagnostics)
        {
            _diagnostics = diagnostics;
        }

        public int Add(int start, int amount)
        {
            try
            {
                int result = checked(start + amount);
                _diagnostics.LogResult(CalculatorOperationText.Add, result);
                return result;
            }
            catch (OverflowException ex)
            {
                throw new CalculatorException(String.Format(ExceptionErrorText.OverflowException, CalculatorOperationText.Add), ex);
            }
        }

        public int Subtract(int start, int amount)
        {
            try
            {
                int result = checked(start - amount);
                _diagnostics.LogResult(CalculatorOperationText.Subtract, result);
                return result;
            }
            catch (OverflowException ex)
            {
                throw new CalculatorException(String.Format(ExceptionErrorText.OverflowException, CalculatorOperationText.Subtract), ex);
            }
        }

        public int Multiply(int start, int by)
        {
            try
            {
                int result = checked(start * by);
                _diagnostics.LogResult(CalculatorOperationText.Multiply, result);
                return result;
            }
            catch (OverflowException ex)
            {
                throw new CalculatorException(String.Format(ExceptionErrorText.OverflowException, CalculatorOperationText.Multiply), ex);
            }
        }

        public int Divide(int start, int by)
        {
            try
            {
                if (by == 0)
                {
                    throw new CalculatorException(ExceptionErrorText.DivideByZeroException);
                }

                int result = start / by;
                _diagnostics.LogResult(CalculatorOperationText.Divide, result);
                return result;
            }
            catch (OverflowException ex)
            {
                throw new CalculatorException(String.Format(ExceptionErrorText.OverflowException, CalculatorOperationText.Divide), ex);
            }
        }
    }
}
