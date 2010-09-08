using System;
using System.Collections.Generic;

namespace InstaTax.Core.DomainObjects{
    public class TaxSlabs{
        private readonly List<TaxSlab> _maleTaxSlabs = new List<TaxSlab>();
        private readonly List<TaxSlab> _femaleTaxSlabs = new List<TaxSlab>();

        private TaxSlabs(){
            _maleTaxSlabs.Add(new TaxSlab(0, 150000, 0));
            _maleTaxSlabs.Add(new TaxSlab(150000, 250000, 10.0));
            _maleTaxSlabs.Add(new TaxSlab(250000, 500000, 20.0));
            _maleTaxSlabs.Add(new TaxSlab(500000, Double.MaxValue, 30.0));

            _femaleTaxSlabs.Add(new TaxSlab(0, 180000, 0));
            _femaleTaxSlabs.Add(new TaxSlab(180000, 250000, 10));
            _femaleTaxSlabs.Add(new TaxSlab(250000, 500000, 20.0));
            _femaleTaxSlabs.Add(new TaxSlab(500000, Double.MaxValue, 30.0));
        }

        private static readonly TaxSlabs Instance = new TaxSlabs();

        public static TaxSlabs GetInstance()
        {
            return Instance;
        }

        public double ComputeTax(double taxableIncome, User taxPayer)
        {
            ValidateTaxComponents(taxableIncome, taxPayer);
            var slabsInUse = GetAppropriateTaxSlabs(taxPayer);
            var tax = 0.00;
            foreach (var slab in slabsInUse){
                tax += slab.GetTax(taxableIncome);
                taxableIncome = taxableIncome - slab.SlabValue;
                if (taxableIncome <= 0)
                    break;
            }

            return tax;
        }

        private void ValidateTaxComponents(double taxableIncome, User taxPayer)
        {
            if (taxPayer == null)
                throw new ArgumentException("Tax payer details not available");

            if (taxableIncome <= 0)
                throw new ArgumentException("Invalid taxable income");
        }

        private List<TaxSlab> GetAppropriateTaxSlabs(User taxPayer)
        {
            if (taxPayer.IsFemale()){
                return _femaleTaxSlabs;
            }
            return _maleTaxSlabs;
        }
    }

    internal class TaxSlab {
        public TaxSlab(double limitLower, double limitUpper, double percentTaxable)
        {
            PercentTaxable = percentTaxable;
            SlabValue = limitUpper - limitLower;
        }

        public double PercentTaxable { get; set; }
        public double SlabValue { get; private set; }

        public double GetTax(double taxableIncome)
        {
            if (taxableIncome > SlabValue)
                taxableIncome = SlabValue;
            return (PercentTaxable * taxableIncome) / 100.00;
        }
    }
}