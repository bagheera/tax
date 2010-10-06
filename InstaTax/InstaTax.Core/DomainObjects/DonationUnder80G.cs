using System;

namespace InstaTax.Core.DomainObjects
{
    public class DonationUnder80G{
        public virtual int Id { get; protected set; }
        
        protected readonly double Amount;

        protected DonationUnder80G(double amount)
        {
            if (amount>=0)
            {
                this.Amount = amount;   
            }
            else
            {
                throw new ArgumentOutOfRangeException("Donation cannot be negative");
            }
            
        }


        public virtual double GetDeduction()
        {
            return this.Amount;
        }
    }

    public class HalfExemptDonation : DonationUnder80G
    {
        private string type = "HALFEXEMPTED";
        public HalfExemptDonation(double amount) : base(amount)
        {
           
        }

        public override double GetDeduction()
        {
            return this.Amount * 0.5;
        }
    }

    public class FullyExemptDonation : DonationUnder80G
    {
        private string type = "FULLYEXEMPTED";
        public FullyExemptDonation(double amount) : base(amount)
        {

        }
    }
}
