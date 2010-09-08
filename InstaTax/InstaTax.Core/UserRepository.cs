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
    }
}