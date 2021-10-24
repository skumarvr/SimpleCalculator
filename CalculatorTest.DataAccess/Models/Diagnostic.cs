using System;
using System.Collections.Generic;

#nullable disable

namespace CalculatorTest.DataAccess.Models
{
    public partial class Diagnostic
    {
        public int Id { get; set; }
        public string Operation { get; set; }
        public int Result { get; set; }
    }
}
