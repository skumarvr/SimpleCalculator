using CalculatorTest.Lib;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace CalculatorTest.App
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Simple Calculator...");

            var collection = new ServiceCollection();
            //collection.AddSingleton<IDiagnostics, DummyDiagnostics>();
            //collection.AddSingleton<IDiagnostics, ConsoleDiagnostics>();
            collection.AddSingleton<IDiagnostics, DatabaseEFDiagnostics>();
            //collection.AddSingleton<IDiagnostics, DatabaseSPDiagnostics>();
            collection.AddScoped<ISimpleCalculator, SimpleCalculator>();

            IServiceProvider serviceProvider = collection.BuildServiceProvider();
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
