using InstaTax.Core;
using NUnit.Framework;

namespace InstaTax.Tests{
    [TestFixture]
    public class TaxSlabsTest{
        [Test]
        public void ShouldReturnZeroTaxIfTaxableIncomeIsLessThanFirstSlab_ForMale(){
            var taxSlabs = new TaxSlabs();

            Assert.AreEqual(0, taxSlabs.ComputeTax(150000, "M"));
        }

        [Test]
        public void ShouldReturnZeroTaxIfTaxableIncomeIsLessThanFirstSlab_ForFemale(){
            var taxSlabs = new TaxSlabs();

            Assert.AreEqual(0, taxSlabs.ComputeTax(180000, "F"));
        }

        [Test]
        public void ShouldReturnTaxBasedOnSecondSlabIfTaxableIncomeFallsWithinSecondSlab_ForMale(){
            var taxSlabs = new TaxSlabs();

            Assert.AreEqual(.1, taxSlabs.ComputeTax(150001, "M"), .01);
        }

        [Test]
        public void ShouldReturnTaxBasedOnSecondAndThirdSlabIfTaxableIncomeFallsWithinThirdSlab_ForMale(){
            var taxSlabs = new TaxSlabs();

            Assert.AreEqual(10000.2, taxSlabs.ComputeTax(250001, "M"), .01);
        }

        [Test]
        public void ShouldReturnTaxBasedOnSecondAndThirdSlabIfTaxableIncomeFallsWithinThirdSlab_ForFemale(){
            var taxSlabs = new TaxSlabs();

            Assert.AreEqual(7000.2, taxSlabs.ComputeTax(250001, "F"), .01);
        }

        [Test]
        public void ShouldReturnTaxBasedOnSecondThirdAndForthSlabIfTaxableIncomeFallsWithinForthSlab_ForMale(){
            var taxSlabs = new TaxSlabs();

            Assert.AreEqual(60000.3, taxSlabs.ComputeTax(500001, "M"), .01);
        }

        [Test]
        public void ShouldReturnTaxBasedOnSecondThirdAndForthSlabIfTaxableIncomeFallsWithinForthSlab_ForFemale(){
            var taxSlabs = new TaxSlabs();

            Assert.AreEqual(57000.3, taxSlabs.ComputeTax(500001, "F"), .01);
        }
    }
}