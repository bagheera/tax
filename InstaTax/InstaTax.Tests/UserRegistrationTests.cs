using InstaTax.Core;
using InstaTax.Core.DomainObjects;
using Moq;
using NUnit.Framework;

namespace InstaTax.Tests{
   [TestFixture, Category("UnitTest")]
    public class RegistrationTests
    {
        [Test]
        public void ShouldRegisterUserIfUnique(){
            var repository = new Mock<IRepository>();

            var password = new Password {PasswordString = "abc"};
            var user = new User(new EmailAddress("a@a.com"), password, repository.Object);

            repository.Setup(rep => rep.LoadByEmailId(user.EmailAddress)).Returns(() => (User) null);
            repository.Setup(rep => rep.Save(user));
            
            user.Register();

            repository.VerifyAll();
        }

        [Test]
        public void ShouldNotRegisterUserIfNotUnique(){
            var repository = new Mock<IRepository>();

            var password = new Password {PasswordString = "abc"};
            var user = new User(new EmailAddress("a@a.com"), password, repository.Object);

            repository.Setup(rep => rep.LoadByEmailId(user.EmailAddress)).Returns(user);

            Assert.Throws<DuplicateUserException>(user.Register);
        }

        

    }
}