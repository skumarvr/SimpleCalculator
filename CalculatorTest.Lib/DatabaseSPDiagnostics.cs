using CalculatorTest.DataAccess;
using CalculatorTest.DataAccess.Models;
using CalculatorTest.Lib.Constants;
using Microsoft.Data.SqlClient;
using System;

namespace CalculatorTest.Lib
{
    public class DatabaseSPDiagnostics : IDiagnostics
    {
        IDbHandler _dbHandler;

        public DatabaseSPDiagnostics(IDbHandler dbHandler)
        {
            _dbHandler = dbHandler;
        }

        public void LogResult(string op, int result)
        {
            try
            {
                var spParams = new SqlParameter[]
                {
                    new SqlParameter("@operation", op),
                    new SqlParameter("@result", result)
                };

                _dbHandler.ExecuteNonQuery("spAddDiagnostic", spParams);
            }
            catch (ArgumentNullException ex)
            {
                throw new CalculatorException(ExceptionErrorText.EmptyConnectionString, ex);
            }
        }
    }
}
