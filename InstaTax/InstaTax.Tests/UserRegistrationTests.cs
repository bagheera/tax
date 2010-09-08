using InstaTax.Core;
using InstaTax.Core.DomainObjects;
using Moq;
using NUnit.Framework;

namespace InstaTax.Tests{
    [TestFixture]
    public class RegistrationTests
    {
        [Test]
        public void ShouldRegisterUserIfUnique(){
            var repository = new Mock<IUserRepository>();
            var password = new Password {PasswordString = "abc"};
            var user = new User("a@a.com", password, repository.Object);
            repository.Setup(rep => rep.Save());
            repository.Setup(rep => rep.CheckIfUnique()).Returns(true);
            user.Save();
        }

        [Test]
        public void ShouldNotRegisterUserIfNotUnique(){
            var repository = new Mock<IUserRepository>();
            var password = new Password {PasswordString = "abc"};
            var user = new User("a@a.com", password, repository.Object);
            repository.Setup(rep => rep.CheckIfUnique()).Returns(false);
            Assert.Throws<DuplicateUserException>(user.Save);
        }

        [Test]
        public void ShouldValidateUserId()
        {
            var repository = new Mock<IUserRepository>();
            var password = new Password {PasswordString = "abc"};
            const string validUserId = "a@a.com";
            const string invalidUserId = "aaa";
            var user = new User(validUserId, password, repository.Object);
            Assert.IsTrue(user.IsValidId());
            user = new User(invalidUserId, password, repository.Object);
            Assert.IsFalse(user.IsValidId());
        }

    }
}