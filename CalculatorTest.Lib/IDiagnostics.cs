using System;
using System.Collections.Generic;
using System.Text;

namespace CalculatorTest.Lib
{
    public interface IDiagnostics
    {
        void LogResult(string op, int result);
    }
}
