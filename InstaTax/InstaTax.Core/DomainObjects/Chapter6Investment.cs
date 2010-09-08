using System;
using System.Collections.Generic;

namespace InstaTax.Core{
    public class Chapter6Investment{
        private readonly List<Investment> investments = new List<Investment>();
        public const int MaximumCapForChapter6 = 100000;

        public double GetDeductions(){
            double totalInvestment = 0;
            foreach (var investment in investments){
                totalInvestment += investment.GetAmount();
            }

            return totalInvestment <= MaximumCapForChapter6 ? totalInvestment : MaximumCapForChapter6;
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
        public const double Cap=70000;

        public PublicProvidentFund(double amount)
            : base(amount){
        }

        public override double GetAmount(){
            return base.GetAmount() <= Cap ? base.GetAmount() : Cap;
        }
    }
}