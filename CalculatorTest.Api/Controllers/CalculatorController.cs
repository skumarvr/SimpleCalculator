using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CalculatorTest.Lib;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CalculatorTest.Api.Controllers
{
    [CustomExceptionFilterAttribute]
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        ISimpleCalculator _simpleCalculator;

        public CalculatorController(ISimpleCalculator simpleCalculator)
        {
            _simpleCalculator = simpleCalculator;
        }

        [HttpGet("Add")]
        public int Add(int start, int amount)
        {
            return _simpleCalculator.Add(start, amount);
        }

        [HttpGet("Subtract")]
        public int Subtract(int start, int amount)
        {
            return _simpleCalculator.Subtract(start, amount);
        }

        [HttpGet("Multiply")]
        public int Multiply(int start, int by)
        {
            return _simpleCalculator.Multiply(start, by);
        }

        [HttpGet("Divide")]
        public int Divide(int start, int by)
        {
            return _simpleCalculator.Divide(start, by);
        }
    }
}
