using System;
using System.Collections.Generic;
using InstaTax.Core.DomainObjects;
using NHibernate;

namespace InstaTax.Core
{
    public class UserRepository : NHibernateSetup, IUserRepository
    {
        public User User { get; set; }

        public UserRepository(User user)
        {
            User = user;
        }

        public bool CheckIfUnique()
        {
            return LoadByEmailId().Count == 0;
        }

        public bool Save()
        {
            if(CheckIfUnique())
            {
                Session.Save(User);
                return true;
            }
            return false;
        }

        public IList<User> LoadByEmailId()
        {
            IQuery query = Session.CreateQuery("from User");
            Console.WriteLine("query created");
            IList<User> people = query.List<User>();
            return people;
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