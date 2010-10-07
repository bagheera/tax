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

        public virtual bool Equals(OtherIncomeItem other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.Id == Id && Equals(other.Name, Name) && other.Amount.Equals(Amount);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (OtherIncomeItem)) return false;
            return Equals((OtherIncomeItem) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = Id;
                result = (result*397) ^ (Name != null ? Name.GetHashCode() : 0);
                result = (result*397) ^ Amount.GetHashCode();
                return result;
            }
        }
    }
}