using System;
using InstaTax.Core;
using InstaTax.Core.DomainObjects;
using NUnit.Framework;

namespace InstaTax.Tests{
    [TestFixture]
    public class AnnualSalaryTest{
        [Test]
        public void ShouldBeAbleToCheckIfTaxPayeeIsFromMetro(){
            var taxPayer = new User(50000, true, Gender.Male);
            Assert.True(taxPayer.FromMetro);
        }

        [Test]
        public void ShouldBeAbleToGetBasicSalary(){
            var annualSalary = new AnnualSalary
                                   {
                                       Basic = 10000.50,
                                       Hra = 1000,
                                       ProfessionalTax = 100,
                                       SpecialAllowance = 10
                                   };
            Assert.AreEqual(10000.50, annualSalary.Basic);
        }

        [Test]
        public void ShouldBeAbleToGetHra(){
            var annualSalary = new AnnualSalary
                                   {
                                       Basic = 10000.50,
                                       Hra = 1000,
                                       ProfessionalTax = 100,
                                       SpecialAllowance = 10
                                   };
            Assert.AreEqual(1000, annualSalary.Hra);
        }

        [Test]
        public void ShouldBeAbleToGetRentPaid(){
            var taxPayer = new User(50000, true, Gender.Male);
            Assert.AreEqual(50000, taxPayer.RentPaid);
        }


        [Test]
        public void ShouldCalculateHraExemption(){
            var taxPayer = new User(50000, true, Gender.Male);
            var annualSalary = new AnnualSalary
                                   {
                                       Basic = 100000,
                                       Hra = 40000,
                                       ProfessionalTax = 100,
                                       SpecialAllowance = 10
                                   };
            Assert.AreEqual(40000, annualSalary.CalculateHraExemption(taxPayer.FromMetro, taxPayer.RentPaid));
        }

        [Test]
        public void ShouldNotCalculateHraExemptionIfBasicSalaryIsNotAvailable(){
            var taxPayer = new User(30000, false, Gender.Male);
            var annualSalary = new AnnualSalary
                                   {
                                       Basic = 0,
                                       Hra = 60000,
                                       ProfessionalTax = 100,
                                       SpecialAllowance = 10
                                   };
            Assert.Throws<Exception>(() => annualSalary.CalculateHraExemption(taxPayer.FromMetro, taxPayer.RentPaid));
        }

        [Test]
        public void ShouldNotCalculateHraExemptionIfHraComponentIsNotAvailable(){
            var taxPayer = new User(30000, true, Gender.Male);
            var annualSalary = new AnnualSalary
                                   {
                                       Basic = 100000,
                                       Hra = 0,
                                       ProfessionalTax = 100,
                                       SpecialAllowance = 10
                                   };
            Assert.Throws<Exception>(() => annualSalary.CalculateHraExemption(taxPayer.FromMetro, taxPayer.RentPaid));
        }

        [Test]
        public void ShouldReturnFiftyPercentageOfBasicAsHraExemptionWhenItIsMinimumOfAllTaxComponentsAndPayerIsFromMetro
            (){
                var taxPayer = new User(65000, true, Gender.Male);
            var annualSalary = new AnnualSalary
                                   {
                                       Basic = 100000,
                                       Hra = 60000,
                                       ProfessionalTax = 100,
                                       SpecialAllowance = 10
                                   };
            Assert.AreEqual(50000, annualSalary.CalculateHraExemption(taxPayer.FromMetro, taxPayer.RentPaid));
        }

        [Test]
        public void ShouldReturnFortyPercentageOfBasicAsHraExemptionWhenItIsMinimumOfAllTaxComponentsAndPayerIsFromMetro
            (){
                var taxPayer = new User(65000, false, Gender.Male);
            var annualSalary = new AnnualSalary
                                   {
                                       Basic = 100000,
                                       Hra = 60000,
                                       ProfessionalTax = 100,
                                       SpecialAllowance = 10
                                   };

            Assert.AreEqual(40000, annualSalary.CalculateHraExemption(taxPayer.FromMetro, taxPayer.RentPaid));
        }

        [Test]
        public void ShouldReturnHraAsHraExemptionWhenHraIsMinimumOfAllTaxComponents(){
            var taxPayer = new User(50000, true, Gender.Male);
            var annualSalary = new AnnualSalary
                                   {
                                       Basic = 100000,
                                       Hra = 20000,
                                       ProfessionalTax = 100,
                                       SpecialAllowance = 10
                                   };
            Assert.AreEqual(20000, annualSalary.CalculateHraExemption(taxPayer.FromMetro, taxPayer.RentPaid));
        }

        [Test]
        public void ShouldReturnRemtPaidAdjustedToBasicAsHraExemptionWhenItIsMinimumOfAllTaxComponents(){
            var taxPayer = new User(30000, true, Gender.Male);
            var annualSalary = new AnnualSalary
                                   {
                                       Basic = 100000,
                                       Hra = 60000,
                                       ProfessionalTax = 100,
                                       SpecialAllowance = 10
                                   };
            Assert.AreEqual(20000, annualSalary.CalculateHraExemption(taxPayer.FromMetro, taxPayer.RentPaid));
        }


        [TestCase((160000))]
        [TestCase((150000))]
        public void ShouldCapHousingLoanInterestExemptionIfAbove1Point5Lac(double testAmount){

            ITaxExemptable housingLoanInterestAmount = new HousingLoanInterest(testAmount);
            Assert.AreEqual(150000, housingLoanInterestAmount.GetAllowedExemption());
        }

        [Test]
        public void ShouldCapHousingLoanInterestExemptionIfBelow1Point5Lac()
        {

            ITaxExemptable housingLoanInterestAmount = new HousingLoanInterest(10000);
            Assert.AreEqual(10000, housingLoanInterestAmount.GetAllowedExemption());
        }


    }
}