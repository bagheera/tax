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
    public class UserPersistenceTest : NHibernateInMemoryTestFixtureBase
    {
        private ISession session;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            InitalizeSessionFactory(new FileInfo(@"..\..\..\InstaTax.Core\DataAccess\User.hbm.xml"));
        }

        [SetUp]
        public void SetUp()
        {
            session = CreateSession();
        }

        [TearDown]
        public void TearDown()
        {
            session.Dispose();
        }
        [Test]
        public void ShouldSaveAndLoadUser()
        {
            var password = new Password { PasswordString = "abc" };
            var user = new User("b@c.com", password);
            IUserRepository repository = new UserRepository(user, session);
            user.Repository = repository;
            repository.Save();
            IList<User> people = repository.LoadByEmailId();  
            Assert.AreEqual(1, people.Count);
            Assert.AreEqual(user.EmailId, people.First().EmailId);
            Assert.IsNotNull(people.First().Id);
        }

        [Test]
        public void ShouldNotSaveIfUserIsNotUnique()
        {
            var password = new Password { PasswordString = "abc" };
            var user = new User("b@c.com", password);
            IUserRepository repository = new UserRepository(user, session);
            user.Repository = repository;
            repository.Save();
            Assert.IsFalse(repository.Save());
        }

        [Test]
        public void ShouldCheckIfUserIsUnique()
        {
            var password = new Password { PasswordString = "abc" };
            var user = new User("b@c.com", password);
            IUserRepository repository = new UserRepository(user, session);
            user.Repository = repository;
            Assert.IsTrue(repository.CheckIfUnique());
            repository.Save();
            Assert.IsFalse(repository.CheckIfUnique());            
        }
    }
}