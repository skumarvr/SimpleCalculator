using System;
using System.Collections.Generic;
using System.Text;

namespace CalculatorTest.Lib
{
    public class DatabaseSPDiagnostics : IDiagnostics
    {
        public void LogResult(string op, int result)
        {
            Console.WriteLine($"{op} : {result}");
        }
    }
}
