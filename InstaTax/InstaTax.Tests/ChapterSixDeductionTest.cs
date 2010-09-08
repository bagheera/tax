using InstaTax.Core;
using NUnit.Framework;

namespace InstaTax.Tests{
    [TestFixture]
    public class ChapterSixDeductionTest{
        [Test]
        public void ShouldReturnZeroDeductionIfInvestmentIsNotAvailable(){
            var salary = new AnnualSalary() {Investments = null};
            Assert.AreEqual(0.0, salary.GetChapter6Deductions(), 0.01);
        }

        [Test]
        public void ShouldReturnZeroDeductionIfNoInvestmentMade(){
            var salary = new AnnualSalary() {Investments = new Chapter6Investment()};
            Assert.AreEqual(0.0, salary.GetChapter6Deductions(), 0.01);
        }

        [Test]
        public void ShouldReturnLifeInsuranceInvestmentAsDeductionIfNoOtherInvestmentIsMade(){
            var investments = new Chapter6Investment();
            investments.Add(new LifeInsurance(30000));
            var salary = new AnnualSalary() {Investments = investments};
            Assert.AreEqual(30000.0, salary.GetChapter6Deductions(), 0.01);
        }

        [Test]
        public void ShouldReturnSumOfLicAndElssIfBothAreWithinCap(){
            var investments = new Chapter6Investment();
            investments.Add(new LifeInsurance(30000));
            investments.Add(new Elss(60000));

            var salary = new AnnualSalary() {Investments = investments};
            Assert.AreEqual(90000.0, salary.GetChapter6Deductions(), 0.01);
        }

        [Test]
        public void ShouldReturnCapValueAsDeductionIfLicAndElssInvestmentsExceedsTheCap() {
            var investments = new Chapter6Investment();
            investments.Add(new LifeInsurance(50000));
            investments.Add(new Elss(60000));

            var salary = new AnnualSalary() { Investments = investments };
            Assert.AreEqual(Chapter6Investment.MaximumCapForChapter6, salary.GetChapter6Deductions(), 0.01);
        }

        [Test]
        public void ShouldReturnTotalPpfInvestmentAsDeductionIfPpfInvestmentIsWithinPpfCapAndNoOtherInvestmentIsMade(){
             var investments = new Chapter6Investment();
            investments.Add(new PublicProvidentFund(69999.99));

            var salary = new AnnualSalary() { Investments = investments };
            Assert.AreEqual(69999.99, salary.GetChapter6Deductions(), 0.01);
        }

        [Test]
        public void ShouldReturnPpfCapAsDeductionIfPpfInvestmentIsAbovePpfCapAndNoOtherInvestmentIsMade()
        {
            var investments = new Chapter6Investment();
            investments.Add(new PublicProvidentFund(70000.1));

            var salary = new AnnualSalary() { Investments = investments };
            Assert.AreEqual(PublicProvidentFund.Cap, salary.GetChapter6Deductions(), 0.01);
        }

        [Test]
        public void ShouldReturnTotalInvestmentAsDeductionIfLicElssAndPpfInvestmentsAreWithinTheCap()
        {
            var investments = new Chapter6Investment();
            investments.Add(new LifeInsurance(50000.09));
            investments.Add(new Elss(30000));
            investments.Add(new PublicProvidentFund(19999.9));

            var salary = new AnnualSalary() { Investments = investments };
            Assert.AreEqual(99999.99, salary.GetChapter6Deductions(), 0.01);
        }
        
        [Test]
        public void ShouldReturnTotalInvestmentCapAsDeductionIfLicElssAndPpfInvestmentsExceedsTheCap()
        {
            var investments = new Chapter6Investment();
            investments.Add(new LifeInsurance(50000));
            investments.Add(new Elss(30000));
            investments.Add(new PublicProvidentFund(20000.01));

            var salary = new AnnualSalary() { Investments = investments };
            Assert.AreEqual(Chapter6Investment.MaximumCapForChapter6, salary.GetChapter6Deductions(), 0.01);
        }


    }
}