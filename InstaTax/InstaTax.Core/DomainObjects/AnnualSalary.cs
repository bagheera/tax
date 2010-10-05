using System;
using System.Collections.Generic;
using System.Linq;

namespace InstaTax.Core.DomainObjects
{

    public class AnnualSalary{

        public virtual double Basic { get; set; }
        public virtual double Hra { get; set; }
        public virtual double SpecialAllowance { get; set; }
        public virtual double ProfessionalTax { get; set; }
        public virtual double Epf { get; set; }

        public virtual string Id { get; set; }

        public virtual double CalculateHraExemption(bool? fromMetro, double rentPaid)
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
            if (fromMetro == null)
                throw new Exception("Locality information is not available");
        }

        private double AdjustedRentPaidToBasic(double rentPaid){
            return rentPaid - Basic * 0.1;
        }

        private double PercentageOfBasicBasedOnLocality(bool? fromMetro){
            if (fromMetro != null && fromMetro.Value)
                return Basic*0.5;
            return Basic*0.4;
        }

        public virtual double GetTaxableSalary()
        {
            return Basic + Hra + SpecialAllowance;
        }
    }
}
