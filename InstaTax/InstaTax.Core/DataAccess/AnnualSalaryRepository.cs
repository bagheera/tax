using System.Collections.Generic;
using NHibernate;

namespace InstaTax.Core
{
    public class AnnualSalaryRepository : NHibernateSetup, IAnnualSalaryRepository
    {
        public void SaveAnnualSalary(AnnualSalary salary)
        {
            Session.Save(salary);
        }

        public AnnualSalary GetAnnualSalary(User user)
        {
            IQuery query = Session.CreateQuery("from AnnualSalary where userId = '" + user.Id + "'");
            IList<AnnualSalary> liSalary = query.List<AnnualSalary>();
            if (liSalary.Count > 0)
                return liSalary[0];

            return null;
        }
    }
}