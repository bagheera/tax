using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InstaTax.Core
{
    public interface IRepository
    {
        Salary getAnnulaSalary(string employeeId, string year);
        HraInfo getHraInfo(string id, string year);
    }
}
