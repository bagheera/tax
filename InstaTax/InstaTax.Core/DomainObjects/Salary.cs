using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InstaTax.Core
{
    public class Salary
    {
        public Salary(double basic, double hra)
        {
            this.basic = basic;
            this.hra = hra;
        }

        public double basic { get; set; }
        public double hra { get; set; }
    }
}
