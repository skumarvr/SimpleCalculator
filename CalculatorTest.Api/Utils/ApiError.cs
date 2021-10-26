using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalculatorTest.Api.Utils
{
    public class ApiError
    {
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public ApiError(string message)
        {
            Message = message;
        }
    }
}
