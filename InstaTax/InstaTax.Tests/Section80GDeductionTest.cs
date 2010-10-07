using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InstaTax.Core.DomainObjects;
using NUnit.Framework;

namespace InstaTax.Tests
{
    [TestFixture, Category("UnitTest")]
    public class Section80GDeductionTest
    {
        [Test]
        public void DeductionShouldBeZeroWhenNoDonationUnder80G()
        {
            DonationsUnder80G donationsUnder80G = new DonationsUnder80G();
            Assert.AreEqual(0,donationsUnder80G.GetDeduction(),0.001);
        }

        [Test]
        public void DeductionForAFullExemptDonationUnder80GShouldbeZeroIfDonationIsZero()
        {
            DonationUnder80G donation = new FullyExemptDonation(0);
            Assert.AreEqual(0, donation.GetDeduction(), 0.001);
        }

        [Test]
        public void DeductionForAHalfExemptDonationUnder80GShouldbeZeroIfDonationIsZero()
        {
            DonationUnder80G donation = new HalfExemptDonation(0);
            Assert.AreEqual(0, donation.GetDeduction(), 0.001);
        }

        [Test]
        public void DeductionForFullExemptDonationShouldBeSameAsTheDonationMade()
        {
            DonationUnder80G donation = new FullyExemptDonation(50000);
            Assert.AreEqual(50000, donation.GetDeduction(), 0.001);
        }


        [Test]
        public void DeductionForAHalfExemptDonationUnder80GShouldbeHalfOfTheDonationMade()
        {
            DonationUnder80G donation = new HalfExemptDonation(50000);
            Assert.AreEqual(25000, donation.GetDeduction(), 0.001);
        }

        [Test]
        public  void ShouldThrowExceptionWhenDonationIsNegative()
        {
            Assert.Throws<ArgumentOutOfRangeException>(delegate
                                                           {
                                                               new HalfExemptDonation(-50000);
                                                           });
        }

        [Test]
        public void DeductionShouldBeEqualToFullyExemptDeductionWhenOneFullyExemptDonationUnder80GIsAdded()
        {
            DonationsUnder80G donationsUnder80G = new DonationsUnder80G();
            donationsUnder80G.AddDonation(new FullyExemptDonation(10));
            Assert.AreEqual(10, donationsUnder80G.GetDeduction(), 0.001);
        }

        [Test]
        public void DeductionShouldBeEqualToHalfOfHalfExemptDeductionWhenOneHalfExemptDonationUnder80GIsAdded()
        {
            DonationsUnder80G donationsUnder80G = new DonationsUnder80G();
            donationsUnder80G.AddDonation(new HalfExemptDonation(10));
            Assert.AreEqual(5, donationsUnder80G.GetDeduction(), 0.001);
        }

        [Test]
        public void DeductionShouldBeEqualToApprpriateDonationUnder80GIsAddedWhenOneFullyExemptAndOneHalfExemptIsAdded()
        {
            DonationsUnder80G donationsUnder80G = new DonationsUnder80G();
            donationsUnder80G.AddDonation(new HalfExemptDonation(10));
            donationsUnder80G.AddDonation(new FullyExemptDonation(10));
            Assert.AreEqual(15, donationsUnder80G.GetDeduction(), 0.001);
        }

        [Test]
        public void TotalDeductionShouldBeEqualToApprpriateSumOfDonationsApplicableToIndividualDonations()
        {
            DonationsUnder80G donationsUnder80G = new DonationsUnder80G();
            donationsUnder80G.AddDonation(new HalfExemptDonation(10));
            donationsUnder80G.AddDonation(new HalfExemptDonation(10));
            donationsUnder80G.AddDonation(new FullyExemptDonation(10));
            donationsUnder80G.AddDonation(new FullyExemptDonation(10));
            Assert.AreEqual(30, donationsUnder80G.GetDeduction(), 0.001);
        }



    }



}
