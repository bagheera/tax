using System;
using System.Collections.Generic;
using System.Linq;

namespace InstaTax.Core.DomainObjects
{
    public class OtherIncomes
    {
        private readonly IList<OtherIncomeItem> otherIncomeItems = new List<OtherIncomeItem>();

        public OtherIncomes()
        {
            // DO NOTHING
        }

        public void Add(OtherIncomeItem otherIncomeItemToAdd)
        {
            if (otherIncomeItemToAdd == null) throw new ArgumentNullException();
            otherIncomeItems.Add(otherIncomeItemToAdd);
        }

        public int Count
        {
            get { return otherIncomeItems.Count; }
        }

        public bool HasItems
        {
            get { return otherIncomeItems.Count > 0; }
        }

        public double CalculateTotalAmount()
        {
            return otherIncomeItems.Sum(a => a.Amount);
        }

        public IEnumerable<OtherIncomeItem> GetItems()
        {
            return otherIncomeItems;
        }
    }
}