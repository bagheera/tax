﻿using System;
using InstaTax.Core;
using InstaTax.Core.DomainObjects;
using NUnit.Framework;

namespace InstaTax.Tests{
    [TestFixture]
    public class AnnualSalaryTest{
        [Test]
        public void ShouldBeAbleToCheckIfTaxPayeeIsFromMetro(){
            var taxPayer = new User(50000, true, Gender.Male);
            Assert.True(taxPayer.FromMetro.Value);
        }

        [Test]
        public void ShouldBeAbleToGetBasicSalary(){
            var taxPayer = new User(50000, true, Gender.Female);
            var annualSalary = new AnnualSalary
                                   {
                                       TaxPayer = taxPayer,
                                       Basic = 10000.50,
                                       Hra = 1000,
                                       ProfessionalTax = 100,
                                       SpecialAllowance = 10
                                   };
            Assert.AreEqual(10000.50, annualSalary.Basic);
        }

        [Test]
        public void ShouldBeAbleToGetHra(){
            var taxPayer = new User(50000, true, Gender.Female);
            var annualSalary = new AnnualSalary
                                   {
                                       TaxPayer = taxPayer,
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
                                       TaxPayer = taxPayer,
                                       Basic = 100000,
                                       Hra = 40000,
                                       ProfessionalTax = 100,
                                       SpecialAllowance = 10
                                   };
            Assert.AreEqual(40000, annualSalary.HraExemption());
        }

        [Test]
        public void ShouldNotCalculateHraExemptionIfBasicSalaryIsNotAvailable(){
            var taxPayer = new User(30000, null, Gender.Male);
            var annualSalary = new AnnualSalary
                                   {
                                       TaxPayer = taxPayer,
                                       Basic = 0,
                                       Hra = 60000,
                                       ProfessionalTax = 100,
                                       SpecialAllowance = 10
                                   };
            Assert.Throws<Exception>(() => annualSalary.HraExemption());
        }

        [Test]
        public void ShouldNotCalculateHraExemptionIfHraComponentIsNotAvailable(){
            var taxPayer = new User(30000, true, Gender.Male);
            var annualSalary = new AnnualSalary
                                   {
                                       TaxPayer = taxPayer,
                                       Basic = 100000,
                                       Hra = 0,
                                       ProfessionalTax = 100,
                                       SpecialAllowance = 10
                                   };
            Assert.Throws<Exception>(() => annualSalary.HraExemption());
        }

        [Test]
        public void ShouldNotCalculateHraExemptionIfTaxPayerIsNotAvailable(){
            var annualSalary = new AnnualSalary
                                   {
                                       TaxPayer = null,
                                       Basic = 100000,
                                       Hra = 0,
                                       ProfessionalTax = 100,
                                       SpecialAllowance = 10
                                   };
            Assert.Throws<Exception>(() => annualSalary.HraExemption());
        }

        [Test]
        public void ShouldNotCalculateHraExemptionIfUserLocalityStatusIsNotAvailable(){
            var taxPayer = new User(30000, null, Gender.Male);
            var annualSalary = new AnnualSalary
                                   {
                                       TaxPayer = taxPayer,
                                       Basic = 100000,
                                       Hra = 60000,
                                       ProfessionalTax = 100,
                                       SpecialAllowance = 10
                                   };
            Assert.Throws<Exception>(() => annualSalary.HraExemption());
        }

        [Test]
        public void ShouldReturnFiftyPercentageOfBasicAsHraExemptionWhenItIsMinimumOfAllTaxComponentsAndPayerIsFromMetro
            (){
                var taxPayer = new User(65000, true, Gender.Male);
            var annualSalary = new AnnualSalary
                                   {
                                       TaxPayer = taxPayer,
                                       Basic = 100000,
                                       Hra = 60000,
                                       ProfessionalTax = 100,
                                       SpecialAllowance = 10
                                   };
            Assert.AreEqual(50000, annualSalary.HraExemption());
        }

        [Test]
        public void ShouldReturnFortyPercentageOfBasicAsHraExemptionWhenItIsMinimumOfAllTaxComponentsAndPayerIsFromMetro
            (){
                var taxPayer = new User(65000, false, Gender.Male);
            var annualSalary = new AnnualSalary
                                   {
                                       TaxPayer = taxPayer,
                                       Basic = 100000,
                                       Hra = 60000,
                                       ProfessionalTax = 100,
                                       SpecialAllowance = 10
                                   };

            Assert.AreEqual(40000, annualSalary.HraExemption());
        }

        [Test]
        public void ShouldReturnHraAsHraExemptionWhenHraIsMinimumOfAllTaxComponents(){
            var taxPayer = new User(50000, true, Gender.Male);
            var annualSalary = new AnnualSalary
                                   {
                                       TaxPayer = taxPayer,
                                       Basic = 100000,
                                       Hra = 20000,
                                       ProfessionalTax = 100,
                                       SpecialAllowance = 10
                                   };
            Assert.AreEqual(20000, annualSalary.HraExemption());
        }

        [Test]
        public void ShouldReturnRemtPaidAdjustedToBasicAsHraExemptionWhenItIsMinimumOfAllTaxComponents(){
            var taxPayer = new User(30000, true, Gender.Male);
            var annualSalary = new AnnualSalary
                                   {
                                       TaxPayer = taxPayer,
                                       Basic = 100000,
                                       Hra = 60000,
                                       ProfessionalTax = 100,
                                       SpecialAllowance = 10
                                   };
            Assert.AreEqual(20000, annualSalary.HraExemption());
        }

        [Test]
        public void ShouldGetNetPayableTax()
        {
            var taxPayer = new User(30000, true, Gender.Male);
            var donationsUnder80G = new DonationsUnder80G();
            donationsUnder80G.AddDonation(new FullyExemptDonation(1000));
            var annualSalary = new AnnualSalary
                                   {
                                       TaxPayer = taxPayer,
                                       Basic = 200000,
                                       Hra = 60000,
                                       ProfessionalTax = 100,
                                       SpecialAllowance = 10,
                                       DonationsUnder80G = donationsUnder80G

            };
            Assert.AreEqual(9891.0, annualSalary.NetPayableTax(),.01);
        }

        [Test]
        public void ShouldExemptHousingLoanInterestFromBeingTaxed(){

            var taxPayer = new User(30000, true, Gender.Male){HousingLoanInterestAmount=10000};
            var annualSalary = new AnnualSalary
            {
                TaxPayer = taxPayer,
                Basic = 200000,
                Hra = 60000,
                ProfessionalTax = 100,
                SpecialAllowance = 10
            };

            Assert.AreEqual(8991.0, annualSalary.NetPayableTax(), .01);
        }
    }
}