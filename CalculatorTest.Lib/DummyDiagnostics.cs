using System;
using System.Collections.Generic;
using System.Text;

namespace CalculatorTest.Lib
{
    public class DummyDiagnostics : IDiagnostics
    {
        public void LogResult(string op, int result)
        {
        }
    }
}
