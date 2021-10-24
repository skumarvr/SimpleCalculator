using System;
using System.Configuration;
using System.Data;
using Microsoft.Data.SqlClient;

namespace CalculatorTest.DataAccess.Models
{
    public class CalculatorDBHandler : IDisposable
    {
        private string _connectionStr = null;

        public CalculatorDBHandler(string connectionStr)
        {
            _connectionStr = connectionStr;
        }

        public void AddDiagnostic(Diagnostic diagnostic)
        {
            if(string.IsNullOrEmpty(_connectionStr.Trim())) 
            {
                throw new ArgumentNullException("Connection String");
            }

            using (SqlConnection con = new SqlConnection(_connectionStr))
            {
                SqlCommand cmd = new SqlCommand("spAddDiagnostic", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@operation", diagnostic.Operation);
                cmd.Parameters.AddWithValue("@result", diagnostic.Result);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void Dispose() { }
    }
}
