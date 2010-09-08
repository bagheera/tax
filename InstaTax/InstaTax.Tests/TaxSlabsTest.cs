using System;
using InstaTax.Core.DomainObjects;
using NUnit.Framework;

namespace InstaTax.Tests{
    [TestFixture]
    public class TaxSlabsTest{
        User testMaleUser = new User(30000, false, Gender.Male);
        User testFemaleUser = new User(30000, false, Gender.Female);
        [Test]
        public void ShouldReturnZeroTaxIfTaxableIncomeIsLessThanFirstSlabForMale(){
            var taxSlabs = TaxSlabs.GetInstance();

            Assert.AreEqual(0, taxSlabs.ComputeTax(150000, testMaleUser ));
        }

        [Test]
        public void ShouldReturnZeroTaxIfTaxableIncomeIsLessThanFirstSlabForFemale(){
            var taxSlabs = TaxSlabs.GetInstance();

            Assert.AreEqual(0, taxSlabs.ComputeTax(180000, testFemaleUser ));
        }

        [Test]
        public void ShouldReturnTaxBasedOnSecondSlabIfTaxableIncomeFallsWithinSecondSlabForMale(){
            var taxSlabs = TaxSlabs.GetInstance();

            Assert.AreEqual(.1, taxSlabs.ComputeTax(150001, testMaleUser ), .01);
        }

        [Test]
        public void ShouldReturnTaxBasedOnSecondAndThirdSlabIfTaxableIncomeFallsWithinThirdSlabForMale(){
            var taxSlabs = TaxSlabs.GetInstance();

            Assert.AreEqual(10000.2, taxSlabs.ComputeTax(250001, testMaleUser ), .01);
        }

        [Test]
        public void ShouldReturnTaxBasedOnSecondAndThirdSlabIfTaxableIncomeFallsWithinThirdSlabForFemale(){
            var taxSlabs = TaxSlabs.GetInstance();

            Assert.AreEqual(7000.2, taxSlabs.ComputeTax(250001, testFemaleUser ), .01);
        }

        [Test]
        public void ShouldReturnTaxBasedOnSecondThirdAndForthSlabIfTaxableIncomeFallsWithinForthSlabForMale(){
            var taxSlabs = TaxSlabs.GetInstance();

            Assert.AreEqual(60000.3, taxSlabs.ComputeTax(500001, testMaleUser ), .01);
        }

        [Test]
        public void ShouldReturnTaxBasedOnSecondThirdAndForthSlabIfTaxableIncomeFallsWithinForthSlabForFemale(){
            var taxSlabs = TaxSlabs.GetInstance();

            Assert.AreEqual(57000.3, taxSlabs.ComputeTax(500001, testFemaleUser ), .01);
        }

        [Test]
        public void ShouldNotCalculateTaxIfTaxPayerDetailsAreNotAvailable()
        {
            var taxSlabs = TaxSlabs.GetInstance();
            Assert.Throws<ArgumentException>(() => taxSlabs.ComputeTax(500000, null));
        }

        [Test]
        public void ShouldNotCalculateTaxIfTaxableIncomeIsInvalid()
        {
            var taxSlabs = TaxSlabs.GetInstance();
            Assert.Throws<ArgumentException>(() => taxSlabs.ComputeTax(-500000, testMaleUser));
        }
    }
}