using CalculatorTest.Lib.Constants;
using System;

namespace CalculatorTest.Lib
{
    public class SimpleCalculator : ISimpleCalculator
    {
        public int Add(int start, int amount)
        {
            try
            {
                return checked(start + amount);
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
                return checked(start - amount);
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
                return checked(start * by);
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
                return start / by;
            }
            catch (OverflowException ex)
            {
                throw new CalculatorException(String.Format(ExceptionErrorText.OverflowException, CalculatorOperationText.Divide), ex);
            }
        }
    }
}
