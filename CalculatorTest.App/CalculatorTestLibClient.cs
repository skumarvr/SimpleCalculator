using CalculatorTest.Lib;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorTest.App
{
    class CalculatorTestLibClient: ICalculatorTestClient
    {
        ISimpleCalculator _simpleCalculator;
        delegate int LibFnDelegate(int num1, int num2);

        public CalculatorTestLibClient(ISimpleCalculator simpleCalculator)
        {
            _simpleCalculator = simpleCalculator;
        }

        public async Task AddAsync(int start, int amount)
        {
            Console.WriteLine($"LIB : Add request : {start}, {amount}");
            LibFnDelegate libFn = _simpleCalculator.Add;
            await InvokeLibFn(libFn, start, amount);
        }

        public async Task SubtractAsync(int start, int amount)
        {
            Console.WriteLine($"LIB : Subtract request : {start}, {amount}");
            LibFnDelegate libFn = _simpleCalculator.Subtract;
            await InvokeLibFn(libFn, start, amount);
        }

        public async Task MultiplyAsync(int start, int by)
        {
            Console.WriteLine($"LIB : Multiply request : {start}, {by}");
            LibFnDelegate libFn = _simpleCalculator.Multiply;
            await InvokeLibFn(libFn, start, by);
        }

        public async Task DivideAsync(int start, int by)
        {
            Console.WriteLine($"LIB : Divide request : {start}, {by}");
            LibFnDelegate libFn = _simpleCalculator.Divide;
            await InvokeLibFn(libFn, start, by);
        }

        private async Task InvokeLibFn(LibFnDelegate libFn, int num1, int num2)
        {
            try
            {
                var result = await Task.FromResult<int>(libFn(num1, num2));
                Console.WriteLine($"LIB : Result : {result}");
            }
            catch(CalculatorException ex)
            {
                Console.WriteLine($"LIB : CalculatorException : {ex.Message}");
            }
            catch(Exception ex)
            {
                Console.WriteLine($"LIB : Exception : {ex.Message}");
            }
        }
    }
}
