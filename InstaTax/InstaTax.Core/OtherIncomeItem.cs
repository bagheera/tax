using System;

namespace InstaTax.Core
{
    public class OtherIncomeItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Amount { get; set; }

        public OtherIncomeItem(string name, double amount)
        {
            if (name == null) throw new ArgumentNullException("name","Name Cannot be Null");
            if (name.Length == 0) throw new ArgumentException("Name must be specified", "name");
            if (amount <= 0.0) throw new ArgumentException("Amount cannot be zero or negative", "amount");
            Name = name;
            Amount = amount;
        }
    }
}