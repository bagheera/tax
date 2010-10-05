namespace InstaTax.Core.DomainObjects{
    public class HousingLoanInterest : ITaxExemptable{

        private const double Cap = 150000;

        private double Amount { get; set; }

        public HousingLoanInterest(double amount){
            Amount = amount;
        }

        public double GetAllowedExemption(){
            return Amount < Cap ? Amount : Cap;
        }
    }
}