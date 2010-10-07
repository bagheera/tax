namespace InstaTax.Core.DomainObjects{
    public class HousingLoanInterest {
        
        
        public  double Amount { get; set; }

        private const double Cap = 150000;

        public HousingLoanInterest()
        {
           
        }

        public HousingLoanInterest(double amount)
        {
            Amount = amount;
        }

        public double GetAllowedExemption(){
            return Amount < Cap ? Amount : Cap;
        }
    }
}