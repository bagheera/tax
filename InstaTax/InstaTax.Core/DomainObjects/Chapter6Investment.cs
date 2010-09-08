using System;
using System.Linq;
using System.Collections.Generic;

namespace InstaTax.Core{
    public class Chapter6Investment{
        private readonly List<Investment> investments = new List<Investment>();
        public const int Cap = 100000;

        public double GetTotal(){
            return investments.Sum(inv => inv.GetAmount());
        }

        public void Add(Investment investment){
            investments.Add(investment);
        }
    }

    public class Investment{
        private readonly double amount;

        protected Investment(double amount){
            this.amount = amount;
        }

        public virtual double GetAmount(){
            return amount;
        }
    }

    public class LifeInsurance : Investment{
        public LifeInsurance(double amount) : base(amount){
        }
    }

    public class Elss : Investment{
        public Elss(double amount)
            : base(amount){
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
    }
}