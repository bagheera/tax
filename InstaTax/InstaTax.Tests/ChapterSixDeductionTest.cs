using System;
using InstaTax.Core;
using NUnit.Framework;

namespace InstaTax.Tests{
    [TestFixture]
    public class ChapterSixDeductionTest{
        [Test]
        public void ShouldReturnZeroDeductionIfInvestmentIsNotAvailableAndEpfIsZero(){
            var salary = new AnnualSalary {Investments = null, Epf = 0.0};
            Assert.AreEqual(0.0, salary.GetChapter6Deductions(), 0.01);

            salary = new AnnualSalary {Investments = new Chapter6Investment()};
            Assert.AreEqual(0.0, salary.GetChapter6Deductions(), 0.01);
        }

        [Test]
        public void ShouldReturnLifeInsuranceInvestmentAsDeductionIfNoOtherInvestmentIsMadeAndEpfIsZero(){
            var investments = new Chapter6Investment();
            investments.Add(new LifeInsurance(30000));
            var salary = new AnnualSalary {Investments = investments, Epf = 0};
            Assert.AreEqual(30000.0, salary.GetChapter6Deductions(), 0.01);
        }

        [Test]
        public void ShouldReturnSumOfLifeInsuranceAndElssIfBothAreWithinCapAndOtherInvestmentAreZero(){
            var investments = new Chapter6Investment();
            investments.Add(new LifeInsurance(30000));
            investments.Add(new Elss(60000));

            var salary = new AnnualSalary {Investments = investments, Epf = 0};
            Assert.AreEqual(90000.0, salary.GetChapter6Deductions(), 0.01);
        }

        [Test]
        public void
            ShouldReturnCapValueAsDeductionIfLifeInsuranceAndElssInvestmentsExceedsTheCapAndOtherInvestmentAreZero(){
            var investments = new Chapter6Investment();
            investments.Add(new LifeInsurance(50000));
            investments.Add(new Elss(60000));

            var salary = new AnnualSalary {Investments = investments, Epf = 0};
            Assert.AreEqual(Chapter6Investment.Cap, salary.GetChapter6Deductions(), 0.01);
        }

        [Test]
        public void ShouldReturnTotalPpfInvestmentAsDeductionIfPpfInvestmentIsWithinPpfCapAndNoOtherInvestmentIsMade(){
            var investments = new Chapter6Investment();
            const double ppfContribution = 69999.99;
            investments.Add(new PublicProvidentFund(ppfContribution));

            var salary = new AnnualSalary {Investments = investments, Epf = 0};
            Assert.AreEqual(ppfContribution, salary.GetChapter6Deductions(), 0.01);
        }

        [Test]
        public void
            ShouldReturnTotalInvestmentAsDeductionIfLicElssAndPpfInvestmentsAreWithinTheInvestmantCapAndEpfIsZero(){
            var investments = new Chapter6Investment();
            investments.Add(new LifeInsurance(50000.09));
            investments.Add(new Elss(30000));
            investments.Add(new PublicProvidentFund(19999.9));

            var salary = new AnnualSalary {Investments = investments, Epf = 0};
            Assert.AreEqual(99999.99, salary.GetChapter6Deductions(), 0.01);
        }

        [Test]
        public void ShouldReturnTotalInvestmentCapAsDeductionIfLicElssAndPpfInvestmentsExceedsTheCapAndEpfIsZero(){
            var investments = new Chapter6Investment();
            investments.Add(new LifeInsurance(50000));
            investments.Add(new Elss(30000));
            investments.Add(new PublicProvidentFund(20000.01));

            var salary = new AnnualSalary { Investments = investments, Epf = 0 };
            Assert.AreEqual(Chapter6Investment.Cap, salary.GetChapter6Deductions(), 0.01);
        }


        [Test]
        public void ShouldReturnNoDeductionIfEpfIsNotAvailableAndNoOtherInvestmentIsMade(){
            var salary = new AnnualSalary {Investments = null, Epf = 0};
            Assert.AreEqual(0, salary.GetChapter6Deductions(), 0.01);
        }

        [Test]
        public void ShouldReturnEpfAsDeductionIfNoOtherInvestmentIsMade(){
            const int epfContribution = 100;
            var salary = new AnnualSalary {Investments = null, Epf = epfContribution};
            Assert.AreEqual(epfContribution, salary.GetChapter6Deductions(), 0.01);
        }

       [Test]
        public void ShouldReturnTotalInvestmentAsDeductionWhenTotalOfAllInvestmetIsUnderDeductionCap(){
            var investments = new Chapter6Investment();
            investments.Add(new LifeInsurance(50000));
            investments.Add(new Elss(30000));
            investments.Add(new PublicProvidentFund(10000));

            var salary = new AnnualSalary {Investments = investments, Epf = 9999.99};
            Assert.AreEqual(99999.99, salary.GetChapter6Deductions(), 0.01);
        }


        [Test]
        public void ShouldReturnDeductionCapAsDeductionWhenTotalOfAllInvestmetIsAboveDeductionCap(){
            var investments = new Chapter6Investment();
            investments.Add(new LifeInsurance(50000));
            investments.Add(new Elss(30000));
            investments.Add(new PublicProvidentFund(10000));

            var salary = new AnnualSalary {Investments = investments, Epf = 500000};
            Assert.AreEqual(Chapter6Investment.Cap, salary.GetChapter6Deductions(), 0.01);
        }


        [Test]
        public void ShouldNotAcceptPpfInvestmentAbovePpfCap(){
            Assert.Throws<ArgumentException>(() => new PublicProvidentFund(70000.01));
        }

    }
}