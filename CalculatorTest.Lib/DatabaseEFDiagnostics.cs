using CalculatorTest.EFDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalculatorTest.Lib
{
    public class DatabaseEFDiagnostics : IDiagnostics
    {
        public void LogResult(string op, int result)
        {
            using (var context = new CalculatorDBContext())
            {

                var dgn = new Diagnostic()
                {
                    Operation = op,
                    Result = result
                };

                context.Diagnostics.Add(dgn);
                context.SaveChanges();
            }
        }
    }
}
