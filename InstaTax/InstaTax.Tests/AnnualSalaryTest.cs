using System;
using InstaTax.Core;
using NUnit.Framework;

namespace InstaTax.Tests{
    [TestFixture]
    public class AnnualSalaryTest{
        [Test]
        public void ShouldBeAbleToCheckIfTaxPayeeIsFromMetro(){
            var taxPayee = new User(50000, true);
            Assert.True(taxPayee.FromMetro.Value);
        }

        [Test]
        public void ShouldBeAbleToGetBasicSalary(){
            var taxPayee = new User(50000, true);
            var annualSalary = new AnnualSalary(taxPayee, 10000.50, 1000, 100, 10);
            Assert.AreEqual(10000.50, annualSalary.Basic);
        }

        [Test]
        public void ShouldBeAbleToGetHra(){
            var taxPayee = new User(50000, true);
            var annualSalary = new AnnualSalary(taxPayee, 10000.50, 1000, 100, 10);
            Assert.AreEqual(1000, annualSalary.Hra);
        }

        [Test]
        public void ShouldBeAbleToGetRentPaid(){
            var taxPayee = new User(50000, true);
            Assert.AreEqual(50000, taxPayee.RentPaid);
        }


        [Test]
        public void ShouldCalculateHraExemption(){
            var taxPayee = new User(50000, true);
            var annualSalary = new AnnualSalary(taxPayee, 100000, 40000, 100, 10);
            Assert.AreEqual(40000, annualSalary.HraExemption());
        }

        [Test]
        public void ShouldNotCalculateHraExemptionIfBasicSalaryIsNotAvailable(){
            var taxPayee = new User(30000, null);
            var annualSalary = new AnnualSalary(taxPayee, 0, 60000, 100, 10);
            Assert.Throws<Exception>(() => annualSalary.HraExemption());
        }

        [Test]
        public void ShouldNotCalculateHraExemptionIfHraComponentIsNotAvailable(){
            var taxPayee = new User(30000, true);
            var annualSalary = new AnnualSalary(taxPayee, 100000, 0, 100, 10);
            Assert.Throws<Exception>(() => annualSalary.HraExemption());
        }

        [Test]
        public void ShouldNotCalculateHraExemptionIfTaxPayerIsNotAvailable(){
            var annualSalary = new AnnualSalary(null, 100000, 0, 100, 10);
            Assert.Throws<Exception>(() => annualSalary.HraExemption());
        }

        [Test]
        public void ShouldNotCalculateHraExemptionIfUserLocalityStatusIsNotAvailable(){
            var taxPayee = new User(30000, null);
            var annualSalary = new AnnualSalary(taxPayee, 100000, 60000, 100, 10);
            Assert.Throws<Exception>(() => annualSalary.HraExemption());
        }

        [Test]
        public void ShouldReturnFiftyPercentageOfBasicAsHraExemptionWhenItIsMinimumOfAllTaxComponentsAndPayerIsFromMetro
            (){
            var taxPayee = new User(65000, true);
            var annualSalary = new AnnualSalary(taxPayee, 100000, 60000, 100, 10);
            Assert.AreEqual(50000, annualSalary.HraExemption());
        }

        [Test]
        public void ShouldReturnFortyPercentageOfBasicAsHraExemptionWhenItIsMinimumOfAllTaxComponentsAndPayerIsFromMetro
            (){
            var taxPayee = new User(65000, false);
            var annualSalary = new AnnualSalary(taxPayee, 100000, 60000, 100, 10);
            Assert.AreEqual(40000, annualSalary.HraExemption());
        }

        [Test]
        public void ShouldReturnHraAsHraExemptionWhenHraIsMinimumOfAllTaxComponents(){
            var taxPayee = new User(50000, true);
            var annualSalary = new AnnualSalary(taxPayee, 100000, 20000, 100, 10);
            Assert.AreEqual(20000, annualSalary.HraExemption());
        }

        [Test]
        public void ShouldReturnRemtPaidAdjustedToBasicAsHraExemptionWhenItIsMinimumOfAllTaxComponents(){
            var taxPayee = new User(30000, true);
            var annualSalary = new AnnualSalary(taxPayee, 100000, 60000, 100, 10);
            Assert.AreEqual(20000, annualSalary.HraExemption());
        }
    }
}