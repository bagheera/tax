using System.Collections.Generic;
using System.IO;
using System.Linq;
using InstaTax.Core;
using InstaTax.Core.DomainObjects;
using NHibernate;
using NUnit.Framework;

namespace InstaTax.Tests
{
    [TestFixture]
    public class TestUserRepository 
    {
        [Test]
        public void ShouldSaveAndLoadUser()
        {
            IUserRepository repository = new UserRepository();

            var password = new Password { PasswordString = "abc" };
            var email = new EmailAddress("b@c.com");
            var user = new User(email, password);

            user.FromMetro = true;
            user.RentPaid = 8000.00;
            
            user.Repository = repository;
            
            user.Register();
            
            User actualUser = repository.LoadByEmailId(email);

            Assert.IsNotNull(actualUser);
            Assert.AreEqual(email, actualUser.EmailAddress);
            Assert.AreEqual(user.FromMetro, actualUser.FromMetro);
            Assert.AreEqual(user.HousingLoanInterest, actualUser.HousingLoanInterest);
            Assert.AreEqual(user.RentPaid, actualUser.RentPaid);
        }
    }
}