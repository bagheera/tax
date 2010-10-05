using System;
using System.Collections.Generic;
using System.Linq;
using InstaTax.Core.DataAccess;
using InstaTax.Core.DomainObjects;

namespace InstaTax.Core{
    public class AnnualSalary{
        public virtual IAnnualSalaryRepository Repository { get; set; }
        public virtual User TaxPayer { get; set; }
        public virtual double Basic { get; set; }
        public virtual double Hra { get; set; }
        public virtual double SpecialAllowance { get; set; }
        public virtual double ProfessionalTax { get; set; }
        private readonly TaxSlabs TaxSlabs = TaxSlabs.GetInstance();
        public virtual double Epf { get; set; }
        private Chapter6Investments _investmentses;
        public virtual string SalaryId
        {
            get; set;
        }

        public virtual string UserId
        {
            get { return TaxPayer.Id; }
            set { TaxPayer.Id = value;}
        }

        public virtual double HraExemption()
        {
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
        }

        private double AdjustedRentPaidToBasic(){
            return TaxPayer.RentPaid - Basic*0.1;
        }

        private double PercentageOfBasicBasedOnLocality(){
            if (TaxPayer.FromMetro)
                return Basic*0.5;
            return Basic*0.4;
        }

        public virtual Chapter6Investments Investmentses{
            set { _investmentses = value; }
        }

        public virtual double GetChapter6Deductions(){
            var totalInvestments = Epf;
            if (_investmentses != null){
                totalInvestments += _investmentses.GetTotal();
            }
            return (totalInvestments <= Chapter6Investments.Cap
                        ? totalInvestments
                        : Chapter6Investments.Cap);
        }

        private double TaxableIncome(){
            return GrossIncome() - HraExemption() - ProfessionalTax - GetChapter6Deductions() - GetHousingLoanInterestAmount();
        }

        private double GetHousingLoanInterestAmount(){
            return TaxPayer.HousingLoanInterestAmount == null ? 0 : TaxPayer.HousingLoanInterestAmount.GetAllowedExemption();
        }

        private double GrossIncome(){
            return Basic + Hra + SpecialAllowance;
        }

        public virtual double NetPayableTax(){
            return TaxSlabs.ComputeTax(TaxableIncome(), TaxPayer);
        }
    }
}
