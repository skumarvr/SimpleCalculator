using CalculatorTest.DataAccess.Models;
using CalculatorTest.Lib.Constants;
using System;

namespace CalculatorTest.Lib
{
    public class DatabaseSPDiagnostics : IDiagnostics
    {
        CalculatorDBHandler _dbHandler;

        public DatabaseSPDiagnostics(CalculatorDBHandler dbHandler)
        {
            _dbHandler = dbHandler;
        }

        public void LogResult(string op, int result)
        {
            try
            {
                _dbHandler.AddDiagnostic(new Diagnostic
                {
                    Operation = op,
                    Result = result
                });
            }
            catch (ArgumentNullException ex)
            {
                throw new CalculatorException(ExceptionErrorText.EmptyConnectionString, ex);
            }
        }
    }
}
