﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InstaTax.Core.DomainObjects;

namespace InstaTax.Core{
    public class AnnualSalary{
        public User TaxPayer { get; set; }
        public double Basic { get; set; }
        public double Hra { get; set; }
        public double SpecialAllowance { get; set; }
        public double ProfessionalTax { get; set; }
        private TaxSlabs TaxSlabs = TaxSlabs.GetInstance();
        private Chapter6Investment investments;

        public double HraExemption(){
            var taxComponents = new List<double>();
            ValidateTaxComponents();
            taxComponents.Add(Hra);
            taxComponents.Add(PercentageOfBasicBasedOnLocality());
            taxComponents.Add(AdjustedRentPaidToBasic());
            return taxComponents.Min();
        }

        private void ValidateTaxComponents(){
            if (Basic <= 0)
                throw new Exception("Basic Salary is not set");
            if (Hra <= 0)
                throw new Exception("HRA is not set");
            if (TaxPayer == null)
                throw new Exception("Tax payer information is not set");
            if (TaxPayer.FromMetro == null)
                throw new Exception("Locality is not set");
        }

        private double AdjustedRentPaidToBasic(){
            return TaxPayer.RentPaid - Basic*0.1;
        }

        private double PercentageOfBasicBasedOnLocality(){
            if (TaxPayer.FromMetro.Value)
                return Basic*0.5;
            else
                return Basic*0.4;
        }

        public Chapter6Investment Investments{
            set { investments = value; }
        }

        public double GetChapter6Deductions(){
            return investments == null ? 0 : investments.GetDeductions();
        }

        private double TaxableIncome(){
            return GrossIncome() - HraExemption() - ProfessionalTax - GetChapter6Deductions();
        }

        private double GrossIncome(){
            return Basic + Hra + SpecialAllowance;
        }

        public double NetPayableTax(){
            return TaxSlabs.ComputeTax(TaxableIncome(), TaxPayer);
        }
    }
}