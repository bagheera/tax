using System;
using System.Collections;
using InstaTax.Core.DomainObjects;
using NUnit.Framework;

namespace InstaTax.Tests
{

    [TestFixture, Category("UnitTest")]
    public class OtherIncomeItemTest
    {
        [Test]
        public void ShouldHaveNameAndValue()
        {
            var item = new OtherIncomeItem("Interest Income",6000.0);

            Assert.AreEqual("Interest Income",item.Name);
            Assert.AreEqual(6000m, item.Amount);
        }

        [Test]
        public void NameShouldNotBeNull()
        {
            Assert.Throws<ArgumentNullException>(() => new OtherIncomeItem(null, 6000.0));
        }
        
        [Test]
        public void NameCannotBeEmpty()
        {
            Assert.Throws<ArgumentException>(() => new OtherIncomeItem("", 6000.0));
        }
        
        [Test]
        public void AmountCannotBeZero()
        {
            Assert.Throws<ArgumentException>(() => new OtherIncomeItem("ABCD", 0.0));
        }
    }
}
