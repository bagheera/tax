using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InstaTax.Core
{
    public interface IAnnualSalaryRepository
    {
        void SaveAnnualSalary(AnnualSalary salary);
        AnnualSalary GetAnnualSalary(User user);
    }
}
