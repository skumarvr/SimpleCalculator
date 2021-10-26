using CalculatorTest.DataAccess;
using CalculatorTest.DataAccess.Models;
using CalculatorTest.Lib;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace CalculatorTest.App
{
    class Program
    {
        static IServiceProvider serviceProvider;

        static void Main(string[] args)
        {
            Console.WriteLine("Simple Calculator...");

            InitialiseServices();

            Console.WriteLine("=== Lib Invocations ===");
            var simpleCalc = serviceProvider.GetService<ISimpleCalculator>();
            CalculatorTestLibClient libClient = new CalculatorTestLibClient(simpleCalc);
            Invoke(libClient);

            Console.WriteLine("=== API Invocations ===");
            var url = ConfigurationManager.AppSettings["ApiUrl"];
            CalculatorTestApiClient apiClient = new CalculatorTestApiClient(url);
            Invoke(apiClient);

            Console.WriteLine("Completed !!!");
            var waitForInput = Console.ReadLine();
        }

        private static void Invoke(ICalculatorTestClient client)
        {   
            Console.WriteLine("");
            client.AddAsync(10,10).GetAwaiter().GetResult();
            Console.WriteLine("");
            client.SubtractAsync(10, 10).GetAwaiter().GetResult();
            Console.WriteLine("");
            client.MultiplyAsync(10, 10).GetAwaiter().GetResult();
            Console.WriteLine("");
            client.DivideAsync(10, 10).GetAwaiter().GetResult();
            Console.WriteLine("");
            client.DivideAsync(10, 0).GetAwaiter().GetResult();
            Console.WriteLine("");
        }

        private static void InitialiseServices()
        {
            var connStr = ConfigurationManager.ConnectionStrings["CalculatorDatabase"].ConnectionString;
            var services = new ServiceCollection();
            services.AddDbContext<CalculatorDBContext>(options => options.UseSqlServer(connStr));
            services.AddScoped<IDbHandler>(x => new CalculatorDBHandler(connStr));
            ////services.AddSingleton<IDiagnostics, DummyDiagnostics>();
            //services.AddSingleton<IDiagnostics, ConsoleDiagnostics>();
            //services.AddScoped<IDiagnostics, DatabaseEFDiagnostics>();
            services.AddScoped<IDiagnostics>(x => new DatabaseSPDiagnostics(x.GetService<IDbHandler>()));
            services.AddScoped<ISimpleCalculator, SimpleCalculator>();

            serviceProvider = services.BuildServiceProvider();
        }
    }
}
