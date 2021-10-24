using CalculatorTest.Lib;
using System;

namespace CalculatorTest.App
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Simpe Calculator!");

            IDiagnostics diag = new ConsoleDiagnostics();
            ISimpleCalculator simpleCalc = new SimpleCalculator(diag);

            simpleCalc.Add(10, 10);
            simpleCalc.Subtract(10, 10);
            simpleCalc.Multiply(10, 10);
            simpleCalc.Divide(10, 10);

            var waitForInput = Console.ReadLine();
        }
    }
}
