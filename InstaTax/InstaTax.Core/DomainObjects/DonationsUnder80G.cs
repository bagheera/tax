using System;
using System.Collections.Generic;

namespace InstaTax.Core.DomainObjects
{
    public class DonationsUnder80G
    {
        protected IList<DonationUnder80G> Donations = new List<DonationUnder80G>();
        public double GetDeduction()
        {
            double total = 0;
            foreach (var donationUnder80G in Donations)
            {
                total += donationUnder80G.GetDeduction();
            }
            return total;
        }

        public void AddDonation(DonationUnder80G donationUnder80G)
        {
            Donations.Add(donationUnder80G);
        }

        public virtual int NumberOfDonations(){
            return Donations.Count;
        }
    }
}
