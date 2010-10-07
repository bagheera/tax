using System;
using System.Collections.Generic;
using InstaTax.Core.DomainObjects;
using NUnit.Framework;

namespace InstaTax.Tests{
    [TestFixture, Category("UnitTest")]
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
        
        [Test]
        public void ShouldGetZeroCountIfNoItemsAreAdded()
        {
            OtherIncomes ois = new OtherIncomes();
            Assert.AreEqual(0, ois.Count);
        }
        
        [Test]
        public void ShouldGetCountIfItemsAreAdded()
        {
            OtherIncomes ois = new OtherIncomes();
            ois.Add(new OtherIncomeItem("Interet", 12000.0));
            ois.Add(new OtherIncomeItem("Rental", 12000.0));
            Assert.AreEqual(2, ois.Count);
        }
        
        [Test]
        public void HasItemsMustReturnTrueIfItemsWereAdded()
        {
            OtherIncomes ois = new OtherIncomes();
            ois.Add(new OtherIncomeItem("Interet", 12000.0));
            ois.Add(new OtherIncomeItem("Rental", 12000.0));
            Assert.IsTrue(ois.HasItems);
        }
        
        [Test]
        public void HasItemsMustReturnFalseIfNoItemsWereAdded()
        {
            OtherIncomes ois = new OtherIncomes();
            Assert.IsFalse(ois.HasItems);
        }

        [Test]
        public void ShouldGetOtherIncomeItemsWhenItemsAreAdded()
        {
            OtherIncomes ois = new OtherIncomes();

            IList<OtherIncomeItem> items = new List<OtherIncomeItem>() { new OtherIncomeItem("Interet", 12000.0),  new OtherIncomeItem("Rental", 12000.0) };
            foreach (OtherIncomeItem otherIncomeItem in items)
            {
                ois.Add(otherIncomeItem);
            }

            Assert.AreEqual(24000.00m, ois.CalculateTotalAmount());
            Assert.IsNotNull(ois.GetItems());
            foreach (OtherIncomeItem otherIncomeItem in ois.GetItems())
            {
                Assert.IsTrue(items.Contains(otherIncomeItem));
            }
        }
    }
}