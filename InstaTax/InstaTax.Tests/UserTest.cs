using InstaTax.Core.DomainObjects;
using NUnit.Framework;

namespace InstaTax.Tests{
    [TestFixture, Category("UnitTest")]
    public class UserTest
    {
        [Test]
        public void ShouldReturnTrueIfTaxPayerIsFemale()
        {
            User taxPayer = new User(0, false, Gender.Female);
            Assert.True(taxPayer.IsFemale());
        }
    }
}