using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorTest.App
{
    interface ICalculatorTestClient
    {
        Task AddAsync(int start, int amount);
        Task SubtractAsync(int start, int amount);
        Task MultiplyAsync(int start, int by);
        Task DivideAsync(int start, int by);
    }
}
