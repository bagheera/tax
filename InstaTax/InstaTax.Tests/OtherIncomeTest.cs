using System;
using InstaTax.Core.DomainObjects;
using NUnit.Framework;

namespace InstaTax.Tests
{

    [TestFixture]
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

    [TestFixture]
    public class OtherIncomesTest
    {
        [Test]
        public void MustBeAbleToAddAnOtherIncomeItem()
        {
            OtherIncomes ois = new OtherIncomes();
            ois.Add(new OtherIncomeItem("ABCD", 12.0));
            Assert.AreEqual(1, ois.Count);
        }
        
        [Test]
        public void CannotAddNullOtherIncomeItem()
        {
            OtherIncomes ois = new OtherIncomes();
            Assert.Throws<ArgumentNullException>(() => ois.Add(null));
        }

        [Test]
        public void ShouldGetTotalAmountForOtherIncomeItems()
        {
            OtherIncomes ois = new OtherIncomes();
            ois.Add(new OtherIncomeItem("Interet", 12000.0));
            ois.Add(new OtherIncomeItem("Rental", 12000.0));
            Assert.AreEqual(24000.00m, ois.CalculateTotalAmount());
          
        }

    }
}
