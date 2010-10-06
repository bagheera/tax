using System;

namespace InstaTax.Core.DomainObjects
{
    public class OtherIncomeItem
    {
        public virtual int Id { get; protected set; }
        public virtual string Name { get; set; }
        public virtual double Amount { get; set; }

        protected OtherIncomeItem()
        {
            // DO NOTHING
        }

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