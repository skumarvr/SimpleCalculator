using CalculatorTest.DataAccess.Models;
using CalculatorTest.Lib;
using CalculatorTest.Lib.Constants;
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
        public void LogResult_saves_Diagnostic_using_CalculatorDBContext()
        {
            var mockSet = new Mock<DbSet<Diagnostic>>();

            var mockContext = new Mock<CalculatorDBContext>(new DbContextOptions<CalculatorDBContext>());
            mockContext.Setup(m => m.Diagnostics).Returns(mockSet.Object);

            var service = new DatabaseEFDiagnostics(mockContext.Object);
            service.LogResult("Test", 100);

            mockSet.Verify(m => m.Add(It.IsAny<Diagnostic>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }
    }
}