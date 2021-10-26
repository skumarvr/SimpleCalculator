using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalculatorTest.DataAccess
{
    public interface IDbHandler
    {
        int ExecuteNonQuery(string storedProcedureName, SqlParameter[] parameters);
    }
}
