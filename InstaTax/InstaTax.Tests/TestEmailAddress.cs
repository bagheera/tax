using System;
using InstaTax.Core;
using InstaTax.Core.DomainObjects;
using NUnit.Framework;

namespace InstaTax.Tests
{
    [TestFixture, Category("UnitTest")]
    public class TestEmailAddress
    {
        [Test]
        public void MustHaveAValue()
        {
            EmailAddress email = new EmailAddress("a@a.com");
            Assert.AreEqual("a@a.com", email.ToString());
        }
        
        [Test]
        public void CannotCreateNullEmailAddress()
        {
            Assert.Throws<ArgumentNullException>(() => new EmailAddress(null));
        }

        [Test]
        public void CannotCreateInvalidEmailAddress()
        {

            Assert.Throws<ArgumentException>(() => new EmailAddress("asdsads"));
        }



    }
}
