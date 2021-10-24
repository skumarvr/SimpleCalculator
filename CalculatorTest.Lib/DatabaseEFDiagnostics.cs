using CalculatorTest.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace CalculatorTest.Lib
{
    public class DatabaseEFDiagnostics : IDiagnostics
    {
        private CalculatorDBContext _dbContext;

        public DatabaseEFDiagnostics(CalculatorDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void LogResult(string op, int result)
        {
            var dgn = new Diagnostic()
            {
                Operation = op,
                Result = result
            };

            _dbContext.Diagnostics.Add(dgn);
            _dbContext.SaveChanges();
        }
    }
}
