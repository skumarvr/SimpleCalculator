using CalculatorTest.ADONETDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalculatorTest.Lib
{
    public class DatabaseSPDiagnostics : IDiagnostics
    {
        public void LogResult(string op, int result)
        {
            using (var context = new CalculatorDBContext())
            {
                context.AddDiagnostic(new ADONETDataAccess.Models.Diagnostic
                {
                    Operation = op,
                    Result = result
                });
            }
        }
    }
}
