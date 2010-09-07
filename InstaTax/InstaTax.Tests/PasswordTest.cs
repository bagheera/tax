using System;
using System.Collections.Generic;
using InstaTax.Core.DomainObjects;
using NUnit.Framework;

namespace InstaTax.Tests
{
    [TestFixture]
    public class PasswordTest
    {

        [Test]
        public void ValidPasswordsShouldContainAtLeastEightCharacters(){
            var password = new Password {PasswordString = "aA!"};
            Assert.IsFalse(password.IsValidPassword());
            password.PasswordString = "aq334454A!";
            Assert.IsTrue(password.IsValidPassword());

        }

        [Test]
        public void ShouldSatisfyAtleastThreeValidationRules()
        {

            
            var password = new Password();

            var passwordToMatchRuleCountDict = new Dictionary<string, int>();

            passwordToMatchRuleCountDict.Add("12345678", 1);
            passwordToMatchRuleCountDict.Add("1234567A", 2);
            passwordToMatchRuleCountDict.Add("1234567a", 2);
            passwordToMatchRuleCountDict.Add("1234567#", 2);
            passwordToMatchRuleCountDict.Add("123456A#", 3);

            foreach (KeyValuePair<String, int> passwordCountPair in passwordToMatchRuleCountDict)
            {
                password.PasswordString = passwordCountPair.Key;
                if (passwordCountPair.Value < 3)
                {
                    Assert.IsFalse(password.IsValidPassword());
                }
                else
                {
                    Assert.IsTrue(password.IsValidPassword());
                }


            }
        }


//        [Test]
//        public void PasswordShouldExpireAfterTheExpiryDuration(){
//            var password = new Password {PasswordString = "twewerer34#"};
//            Assert.False(password.isExpired());
//        }
    }

}
