using System;
using System.Collections.Generic;
using System.Linq;
using InstaTax.Core.DomainObjects;

namespace InstaTax.Core {

    public class AnnualSalary{

        public virtual double Basic { get; set; }
        public virtual double Hra { get; set; }
        public virtual double SpecialAllowance { get; set; }
        public virtual double ProfessionalTax { get; set; }
        public virtual double Epf { get; set; }
        public virtual double TaxDedeuctedAtSource { get; set; }

        public virtual string SalaryId { get; set; }

        

        public virtual double CalculateHraExemption(bool fromMetro, double rentPaid)
        {
            var taxComponents = new List<double>();
            ValidateTaxComponents(fromMetro);
            taxComponents.Add(Hra);
            taxComponents.Add(PercentageOfBasicBasedOnLocality(fromMetro));
            taxComponents.Add(AdjustedRentPaidToBasic(rentPaid));
            return taxComponents.Min();
        }

        private void ValidateTaxComponents(bool? fromMetro){
            if (Basic <= 0)
                throw new Exception("Basic Salary is not set");
            if (Hra <= 0)
                throw new Exception("HRA is not set");
        }

        private double AdjustedRentPaidToBasic(double rentPaid){
            if (rentPaid > (Basic * 0.1))
            {
                return rentPaid - Basic * 0.1;               
            }
            else
            {
                return rentPaid;
            }
 
        }

        private double PercentageOfBasicBasedOnLocality(bool fromMetro){
            if (fromMetro)
                return Basic*0.5;
            return Basic*0.4;
        }


        public double GetTaxableSalary()
        {
            return Basic + Hra + SpecialAllowance;
        }
    }
}
