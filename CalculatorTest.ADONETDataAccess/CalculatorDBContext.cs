using CalculatorTest.ADONETDataAccess.Models;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace CalculatorTest.ADONETDataAccess
{
    public class CalculatorDBContext : IDisposable
    {
        private bool _disposedValue;

        private string _connectionStr = ConfigurationManager.ConnectionStrings["CalculatorDatabase"].ConnectionString;

        public void AddDiagnostic(Diagnostic diagnostic)
        {
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
