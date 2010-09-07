using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InstaTax.Core
{
    public class Hra
    {
        private IRepository repository;
        private string employeeId;
        private string financialYear;
        public Hra(string employeeId, string financialYear)
        {
            this.employeeId = employeeId;
            this.financialYear = financialYear;
        }

        public void getExcemptedAmount()
        {
            //Check if provided finanacial year is less or equal to current year
            if (int.Parse(financialYear) > DateTime.Now.Year)
            {
                throw new ArgumentException("Invalid financial year");
            }

            //Check if salary is available for given employee
            Salary salary = repository.getAnnulaSalary(employeeId, financialYear);
            if (salary == null)
                throw new Exception("No Salary Available");

            //Check if rent paid is available
            HraInfo hraInfo = repository.getHraInfo(employeeId, financialYear);
            if (hraInfo == null)
                throw new Exception("HRA information is missing for employeeid:" + employeeId);

        }

        public void setRepository(IRepository repository)
        {
            this.repository = repository;
        }
    }
}
