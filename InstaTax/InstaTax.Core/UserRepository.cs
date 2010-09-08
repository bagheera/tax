using System;
using System.Collections.Generic;
using InstaTax.Core.DomainObjects;
using NHibernate;

namespace InstaTax.Core
{
    public class UserRepository : IUserRepository
    {
        public User User { get; set; }
        public ISession Session { get; set; }

        public UserRepository(User user, ISession session)
        {
            User = user;
            Session = session;
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