using System;
using System.Collections.Generic;

namespace InstaTax.Core.DomainObjects
{
    public class DonationsUnder80G
    {
        private List<DonationUnder80G> donations = new List<DonationUnder80G>();
        public double GetDeduction()
        {
            double total = 0;
            foreach (var donationUnder80G in donations)
            {
                total += donationUnder80G.GetDeduction();
            }
            return total;
        }

        public void AddDonation(DonationUnder80G donationUnder80G)
        {
            donations.Add(donationUnder80G);
        }
    }
}
