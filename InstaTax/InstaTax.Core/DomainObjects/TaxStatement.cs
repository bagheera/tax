using System;
using InstaTax.Core.DomainObjects;

namespace InstaTax.Core
{
    public class TaxStatement
    {
        public AnnualSalary AnnualSalary { get; set; }

        public OtherIncomes OtherIncomes { get; set; }

        public Chapter6Investment Chapter6Investments { get; set; }

        private DonationsUnder80G donationsUnder80G = new DonationsUnder80G();
        public virtual DonationsUnder80G DonationsUnder80G
        {
            protected get { return donationsUnder80G; }
            set { donationsUnder80G = value ?? new DonationsUnder80G(); }
        }


        public TaxStatement(AnnualSalary annualSalary)
        {
            AnnualSalary = annualSalary;
        }

        private double CalculateGrossIncome(User taxPayer)
        {
            double retAmt = AnnualSalary.GetTaxableSalary();

            retAmt -= taxPayer.HousingLoanInterestAmount;

            if (OtherIncomes != null)
                retAmt += OtherIncomes.CalculateTotalAmount();
                
            retAmt -= (AnnualSalary.CalculateHraExemption(taxPayer.FromMetro, taxPayer.RentPaid) +
                AnnualSalary.ProfessionalTax);

            retAmt -= DonationsUnder80G.GetDeduction();
            
            return retAmt;
        }

        public virtual double GetChapter6Deductions()
        {
            var totalInvestments = AnnualSalary.Epf;
            if (Chapter6Investments != null)
            {
                totalInvestments += Chapter6Investments.GetTotal();
            }

            return (totalInvestments <= Chapter6Investment.Cap
                        ? totalInvestments
                        : Chapter6Investment.Cap);
        }


        public double CalculateNetPayableTax(User taxPayer)
        {
            double netTaxableIncome = CalculateGrossIncome(taxPayer)
                                      - GetChapter6Deductions();
            
            return TaxSlabs.GetInstance().ComputeTax(netTaxableIncome, taxPayer);
        }
    }
}