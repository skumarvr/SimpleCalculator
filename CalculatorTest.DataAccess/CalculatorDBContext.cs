using System;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.IdentityModel.Protocols;

namespace CalculatorTest.DataAccess.Models
{
    public partial class CalculatorDBContext : DbContext
    {
        public CalculatorDBContext(DbContextOptions<CalculatorDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Diagnostic> Diagnostics { get; set; }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
