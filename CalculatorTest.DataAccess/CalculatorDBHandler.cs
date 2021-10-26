using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using Microsoft.Data.SqlClient;

namespace CalculatorTest.DataAccess.Models
{
    public class CalculatorDBHandler : IDisposable, IDbHandler
    {
        private readonly string _connectionStr;

        public CalculatorDBHandler(string connectionStr)
        {
            _connectionStr = connectionStr;
        }

        public void Dispose() 
        {
        }

        public int ExecuteNonQuery(string storedProcedureName, SqlParameter[] parameters)
        {
            int retVal = -1;
            using (SqlConnection con = CreateConnection(_connectionStr))
            {
                SqlCommand cmd = new SqlCommand(storedProcedureName, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddRange(parameters);

                con.Open();
                retVal = cmd.ExecuteNonQuery();
                con.Close();
            }
            return retVal;
        }

        private SqlConnection CreateConnection(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString.Trim()))
            {
                throw new ArgumentNullException("Connection String");
            }

            return new SqlConnection(connectionString);
        }
    }
}
