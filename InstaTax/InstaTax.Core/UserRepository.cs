using System;
using System.Collections.Generic;
using InstaTax.Core.DomainObjects;
using NHibernate;

namespace InstaTax.Core
{
    public class UserRepository : NHibernateSetup, IUserRepository
    {
        public UserRepository()
        {
        }

        public void Save(User u)
        {
            Session.Save(u);
        }

        public User LoadByEmailId(EmailAddress emailAddress)
        {
            IQuery query = Session.CreateQuery("from User u WHERE u.EmailAddress = :emailAddress");
            query.SetParameter("emailAddress", emailAddress.ToString());

            return query.UniqueResult<User>();
        }

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