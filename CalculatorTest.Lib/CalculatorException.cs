using System;
using System.Collections.Generic;
using System.Text;

namespace CalculatorTest.Lib
{
    public class CalculatorException : ApplicationException
    {
        public CalculatorException(string txt) : base(txt)
        {
        }

        public CalculatorException(string txt, Exception ex) : base(txt, ex)
        {
        }
    }
}
