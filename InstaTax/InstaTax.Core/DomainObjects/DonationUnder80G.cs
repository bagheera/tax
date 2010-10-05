using System;

namespace InstaTax.Core.DomainObjects
{
    public class DonationUnder80G
    {
        protected readonly double amount;

        protected DonationUnder80G(double amount)
        {
            if (amount>=0)
            {
                this.amount = amount;   
            }
            else
            {
                throw new ArgumentOutOfRangeException("Donation cannot be negative");
            }
            
        }


        public virtual double GetDeduction()
        {
            return this.amount;
        }
    }

    public class HalfExemptDonation : DonationUnder80G
    {
        public HalfExemptDonation(double amount) : base(amount)
        {
           
        }

        public override double GetDeduction()
        {
            return this.amount*0.5;
        }
    }

    public class FullyExemptDonation : DonationUnder80G
    {
        public FullyExemptDonation(double amount) : base(amount)
        {

        }
    }
}
