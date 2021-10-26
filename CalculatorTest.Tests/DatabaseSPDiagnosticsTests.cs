using CalculatorTest.DataAccess;
using CalculatorTest.DataAccess.Models;
using CalculatorTest.Lib;
using CalculatorTest.Lib.Constants;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Linq.Expressions;

namespace Calculator.Tests
{
    public class DatabaseSPDiagnosticsTests
    {
        [Test]
        public void LogResult_saves_Diagnostic_using_CalculatorDBHandler()
        {
            var mockDbHandler = new Mock<IDbHandler>();
            mockDbHandler.Setup(m => m.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<SqlParameter[]>()))
                        .Returns(1);

            var service = new DatabaseSPDiagnostics(mockDbHandler.Object);
            service.LogResult("Test", 100);

            mockDbHandler.Verify(m => m.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<SqlParameter[]>()), Times.Once());
        }
    }
}