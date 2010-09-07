using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InstaTax.Core{
    public class AnnualSalary{
        public User TaxPayer { get; set; }
        public double Basic { get; set; }
        public double Hra { get; set; }
        public double SpecialAllowance { get; set; }
        public double ProfessionalTax { get; set; }

        //public AnnualSalary(User taxPayer, double basic, double hra, double specialAllowance, double professionalTax)
        //{
        //    TaxPayer = taxPayer;
        //    Basic = basic;
        //    Hra = hra;
        //    SpecialAllowance = specialAllowance;
        //    ProfessionalTax = professionalTax;
        //}


        public double HraExemption(){
            List<double> taxComponents = new List<double>();
            ValidateTaxComponents();
            taxComponents.Add(Hra);
            taxComponents.Add(GetPercentageOfBasicBasedOnLocality());
            taxComponents.Add(GetAdjustedRentPaidToBasic());
            return taxComponents.Min();
        }

        private void ValidateTaxComponents(){
            if (Basic == 0)
                throw new Exception("Basic Salary is not set");
            if (Hra == 0)
                throw new Exception("HRA is not set");
            if (TaxPayer == null)
                throw new Exception("Tax payer information is not set");
            if (TaxPayer.FromMetro == null)
                throw new Exception("Locality is not set");
        }

        private double GetAdjustedRentPaidToBasic(){
            return TaxPayer.RentPaid - Basic*0.1;
        }

        private double GetPercentageOfBasicBasedOnLocality(){
            if (TaxPayer.FromMetro.Value)
                return Basic*0.5;
            else
                return Basic*0.4;
        }
    }
}