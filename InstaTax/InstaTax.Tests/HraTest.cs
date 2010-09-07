﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using NUnit.Framework;
using InstaTax.Core;

namespace InstaTax.Tests
{
    [TestFixture]
    public class HraCalulatorTest
    {
        [Test, ExpectedException(typeof(ArgumentException))]
        public void ProperFinancialYearShouldbeSupplied()
        {
            Hra hra = new Hra("e370881", "2011");
            hra.getExcemptedAmount();
        }

        [Test, ExpectedException(typeof(Exception))]
        public void SalaryBreakupForUserShouldBeAvailable()
        {
            string employeeId = "e370881";
            string financialYear = "2010";
            Mock<IRepository> mockRepository = new Mock<IRepository>();
            Salary salary = null;
            mockRepository.Setup(repository => repository.getAnnulaSalary(employeeId, financialYear)).Returns(salary);

            Hra hra = new Hra(employeeId, financialYear);
            hra.setRepository(mockRepository.Object);
            hra.getExcemptedAmount();
        }

        [Test, ExpectedException(typeof(Exception))]
        public void RentPaidShouldBeAvailableBeforeCalculatingHRA()
        {

            string employeeId = "e370881";
            string financialYear = "2010";
            Mock<IRepository> mockRepository = new Mock<IRepository>();
            Salary salary = new Salary(1500, 100);
            mockRepository.Setup(repository => repository.getAnnulaSalary(employeeId, financialYear)).Returns(salary);
            HraInfo hraInfo = null;
            mockRepository.Setup(repository => repository.getHraInfo(employeeId, financialYear)).Returns(hraInfo);

            Hra hra = new Hra(employeeId, financialYear);
            hra.setRepository(mockRepository.Object);
            hra.getExcemptedAmount();
        }
    }
}