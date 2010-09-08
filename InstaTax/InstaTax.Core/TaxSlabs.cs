using System;
using System.Collections.Generic;


namespace InstaTax.Core{
    public class TaxSlabs{
        private readonly List<TaxSlab> maleTaxSlabs = new List<TaxSlab>();
        private readonly List<TaxSlab> femaleTaxSlabs = new List<TaxSlab>();

        public TaxSlabs(){
            maleTaxSlabs.Add(new TaxSlab(0, 150000, 0));
            maleTaxSlabs.Add(new TaxSlab(150000, 250000, 10.0));
            maleTaxSlabs.Add(new TaxSlab(250000, 500000, 20.0));
            maleTaxSlabs.Add(new TaxSlab(500000, Double.MaxValue, 30.0));

            femaleTaxSlabs.Add(new TaxSlab(0, 180000, 0));
            femaleTaxSlabs.Add(new TaxSlab(180000, 250000, 10));
            femaleTaxSlabs.Add(new TaxSlab(250000, 500000, 20.0));
            femaleTaxSlabs.Add(new TaxSlab(500000, Double.MaxValue, 30.0));
        }

        public double ComputeTax(double taxableIncome, string gender){
            var slabInUse = maleTaxSlabs;

            var untaxedSalary = taxableIncome;
            var tax = 0.00;

            if ("F".Equals(gender)){
                slabInUse = femaleTaxSlabs;
            }
            foreach (var slab in slabInUse){
                var slabValue = slab.LimitUpper - slab.LimitLower;

                if (untaxedSalary > slabValue)
                    tax += (slab.PercentTaxable*slabValue)/100.00;
                else{
                    tax += (slab.PercentTaxable*untaxedSalary)/100.00;
                }
                untaxedSalary = untaxedSalary - slabValue;
                if (untaxedSalary <= 0){
                    break;
                }
            }

            return tax;
        }
    }

    internal class TaxSlab {
        public TaxSlab(double limitLower, double limitUpper, double percentTaxable)
        {
            LimitLower = limitLower;
            LimitUpper = limitUpper;
            PercentTaxable = percentTaxable;
        }

        public double LimitLower { get; set; }
        public double LimitUpper { get; set; }
        public double PercentTaxable { get; set; }
    }
}