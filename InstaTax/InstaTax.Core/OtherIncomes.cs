using System;
using System.Collections.Generic;
using System.Linq;

namespace InstaTax.Core
{
    public class OtherIncomes
    {
        private readonly List<OtherIncomeItem> otherIncomeItems = new List<OtherIncomeItem>();

        public void Add(OtherIncomeItem otherIncomeItemToAdd)
        {
            if (otherIncomeItemToAdd == null) throw new ArgumentNullException();
            otherIncomeItems.Add(otherIncomeItemToAdd);
        }

        public int Count
        {
            get { return otherIncomeItems.Count; }
        }

        public double CalculateTotalAmount()
        {
            return otherIncomeItems.Sum(a => a.Amount);
        }
    }
}