using System;

namespace InstaTax.Core.DomainObjects
{
    public abstract class DonationUnder80G{
        protected virtual TaxStatement TaxStatement { get; set; }
        public virtual int Id { get; protected set; }
        
        protected double Amount;
        protected  DonationUnder80G(){
           
        }
        protected DonationUnder80G(double amount, TaxStatement taxStatement){
            TaxStatement = taxStatement;
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

        private bool Equals(DonationUnder80G other){
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.Id == Id;
        }

        public override bool Equals(object obj){
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (DonationUnder80G)) return false;
            return Equals((DonationUnder80G) obj);
        }

        public override int GetHashCode(){
            return Id;
        }
    }

    public class HalfExemptDonation : DonationUnder80G
    {
        private string type = "HALFEXEMPTED";
        public HalfExemptDonation(double amount) : base(amount, null)
        {
           
        }

        public override double GetDeduction()
        {
            return this.Amount * 0.5;
        }
        protected HalfExemptDonation() : base(){
            
        }
        protected HalfExemptDonation(double amount, TaxStatement taxStatement) : base(amount, taxStatement){
            
        }
    }

    public class FullyExemptDonation : DonationUnder80G
    {
        private string type = "FULLYEXEMPTED";
        public FullyExemptDonation(double amount) : base(amount, null)
        {

        }
        protected FullyExemptDonation() : base(){
            
        }
        protected FullyExemptDonation(double amount, TaxStatement taxStatement)
            : base(amount, taxStatement)
        {
            
        }
    }
}
