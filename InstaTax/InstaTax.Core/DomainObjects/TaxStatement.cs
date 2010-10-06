namespace InstaTax.Core.DomainObjects
{
    public class TaxStatement
    {
        protected  TaxStatement(){
            
        }
        public virtual int Id { get; protected set; }
        
        public virtual AnnualSalary AnnualSalary { get; set; }

        public virtual ITaxExemptable HousingLoanInterest { get; set; }

        public virtual OtherIncomes OtherIncomes { get; set; }

        public virtual Chapter6Investments Chapter6Investments { get; set; }

        private DonationsUnder80G donationsUnder80G = new DonationsUnder80G();

        private User TaxPayer { get; set; }

        public virtual DonationsUnder80G DonationsUnder80G
        {
            protected get { return donationsUnder80G; }
            set { donationsUnder80G = value ?? new DonationsUnder80G(); }
        }




        public TaxStatement(AnnualSalary annualSalary, User taxPayer)
        {
            AnnualSalary = annualSalary;
            TaxPayer = taxPayer;
        }

        private double CalculateGrossIncome(User taxPayer)
        {
            double retAmt = AnnualSalary.GetTaxableSalary();

            retAmt -= GetHousingLoanInterestAmount();

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

            return (totalInvestments <= Chapter6Investments.Cap
                        ? totalInvestments
                        : Chapter6Investments.Cap);
        }

        private double GetHousingLoanInterestAmount()
        {
            return HousingLoanInterest == null ? 0 : HousingLoanInterest.GetAllowedExemption();
        }

        public virtual double CalculateNetPayableTax()
        {
            double netTaxableIncome = CalculateGrossIncome(TaxPayer)
                                      - GetChapter6Deductions();
            
            return TaxSlabs.GetInstance().ComputeTax(netTaxableIncome, TaxPayer)-(AnnualSalary.ProfessionalTax + AnnualSalary.TaxDedeuctedAtSource);
        }

        public virtual int NumberOfDonations(){
            return DonationsUnder80G.NumberOfDonations();
        }
    }
}