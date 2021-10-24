using CalculatorTest.DataAccess.Models;
using CalculatorTest.Lib;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Configuration;
using System.Threading.Tasks;

namespace CalculatorTest.App
{
    class Program
    {
        public static object Configuration { get; private set; }

        static void Main(string[] args)
        {
            Console.WriteLine("Simple Calculator...");

            var connStr = ConfigurationManager.ConnectionStrings["CalculatorDatabase"].ConnectionString;
            var services = new ServiceCollection();
            services.AddDbContext<CalculatorDBContext>(options => options.UseSqlServer(connStr));
            services.AddScoped<CalculatorDBHandler>(x => new CalculatorDBHandler(connStr));
            //services.AddSingleton<IDiagnostics, DummyDiagnostics>();
            //services.AddSingleton<IDiagnostics, ConsoleDiagnostics>();
            //services.AddScoped<IDiagnostics, DatabaseEFDiagnostics>();
            services.AddScoped<IDiagnostics>(x => new DatabaseSPDiagnostics(x.GetService<CalculatorDBHandler>()));
            services.AddScoped<ISimpleCalculator, SimpleCalculator>();
            
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            var simpleCalc = serviceProvider.GetService<ISimpleCalculator>();

            simpleCalc.Add(10, 10);
            simpleCalc.Subtract(10, 10);
            simpleCalc.Multiply(10, 10);
            simpleCalc.Divide(10, 10);

            Console.WriteLine("Completed !!!");
            var waitForInput = Console.ReadLine();
        }
    }
}
