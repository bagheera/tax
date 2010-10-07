using System;
using System.Linq;
using System.Collections.Generic;

namespace InstaTax.Core.DomainObjects{
    public class Chapter6Investments {
        private readonly IList<Investment> investments = new List<Investment>();
        public const int Cap = 100000;

        public double GetTotal(){
            return investments.Sum(inv => inv.GetAmount());
        }

        public void Add(Investment investment){
            investments.Add(investment);
        }
    }

    public class Investment {

        public virtual  int Id { get; set; }
        private readonly double amount;
        public virtual TaxStatement TaxStatement { get; set; }
        protected Investment(double amount){
            this.amount = amount;
        }

        public virtual double GetAmount(){
            return amount;
        }

        protected Investment(){
            
        }
    }

    public class LifeInsurance : Investment{
        public LifeInsurance(double amount) : base(amount){
        }

        protected LifeInsurance(){
            
        }
    }

    public class Elss : Investment{
        public Elss(double amount)
            : base(amount){
        }

        protected Elss(){
            
        }
    }

    public class PublicProvidentFund : Investment{
        public const double Cap = 70000;

        public PublicProvidentFund(double amount)
            : base(amount){
            if (amount > Cap) throw new ArgumentException("PPF cannot exceed cap: " + Cap);
        }

        public override double GetAmount(){
            return base.GetAmount() <= Cap ? base.GetAmount() : Cap;
        }

        protected PublicProvidentFund(){
            
        }
    }

    public class HousingLoanPrincipal : Investment
    {
        public HousingLoanPrincipal(double amount)
            : base(amount)
        {
            
        }

        protected HousingLoanPrincipal(){
            
        }
    }
}