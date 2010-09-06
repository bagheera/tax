using System;
using System.Collections.Generic;
using InstaTax.Core.DomainObjects;
using NUnit.Framework;

namespace InstaTax.Tests
{
    [TestFixture]
    public class UserAuthenticatorTest
    {

        [Test]
        public void ValidPasswordsShouldContainAtLeastEightCharacters(){
            UserAuthenticator userAuthenticator = new UserAuthenticator();
            userAuthenticator.Password = "aA!";
            Assert.IsFalse(userAuthenticator.IsValidPassword());

            userAuthenticator.Password = "aq334454A!";
            Assert.IsTrue(userAuthenticator.IsValidPassword());

        }

        [Test]
        public void ShouldSatisfyAtleastThreeValidationRules()
        {

            
            var authentication = new UserAuthenticator();

            Dictionary<String, int> passwordToMatchRuleCountDict = new Dictionary<string, int>();

            passwordToMatchRuleCountDict.Add("12345678", 1);
            passwordToMatchRuleCountDict.Add("1234567A", 2);
            passwordToMatchRuleCountDict.Add("1234567a", 2);
            passwordToMatchRuleCountDict.Add("1234567#", 2);
            passwordToMatchRuleCountDict.Add("123456A#", 3);

            foreach (KeyValuePair<String, int> passwordCountPair in passwordToMatchRuleCountDict)
            {
                authentication.Password = passwordCountPair.Key;
                if (passwordCountPair.Value < 3)
                {
                    Assert.IsFalse(authentication.IsValidPassword());
                }
                else
                {
                    Assert.IsTrue(authentication.IsValidPassword());
                }


            }
        }

    }

}
