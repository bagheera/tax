using System;
using System.Collections.Generic;
using InstaTax.Core.DomainObjects;
using NUnit.Framework;

namespace InstaTax.Tests{
    [TestFixture]
    public class PasswordTest{
        [Test]
        public void ValidPasswordsShouldContainAtLeastEightCharacters(){
            var password = new Password {PasswordString = "aA!"};
            Assert.IsFalse(password.IsValidPassword());
            password.PasswordString = "aq334454A!";
            Assert.IsTrue(password.IsValidPassword());
        }

        [Test]
        public void ShouldSatisfyAtleastThreeValidationRules(){
            var password = new Password();

            var passwordToMatchRuleCountDict = new Dictionary<string, int>
                                                   {
                                                       {"12345678", 1},
                                                       {"1234567A", 2},
                                                       {"1234567a", 2},
                                                       {"1234567#", 2},
                                                       {"123456A#", 3}
                                                   };

            foreach (KeyValuePair<String, int> passwordCountPair in passwordToMatchRuleCountDict){
                password.PasswordString = passwordCountPair.Key;
                if (passwordCountPair.Value < 3){
                    Assert.IsFalse(password.IsValidPassword());
                }
                else{
                    Assert.IsTrue(password.IsValidPassword());
                }
            }
        }


        [Test]
        public void PasswordShouldExpireAfterTheExpiryDuration(){
            var password = new Password {PasswordString = "twewerer34#"};
            password.CreatedOn = DateTime.Today.AddDays(-91);
            Assert.True(password.IsExpired());

            password.CreatedOn = DateTime.Today.AddDays(-90);
            Assert.False(password.IsExpired());

            password.CreatedOn = DateTime.Today.AddDays(-56);
            Assert.False(password.IsExpired());
        }

        [Test]
        public void ReminderMailShouldBeSentOneWeekBeforeExpiration(){
            
            var password = new Password {PasswordString = "twewerer34#"};
            password.CreatedOn = DateTime.Today.AddDays(-84);
            password.ExpiryNotificationSent = false;
            password.SendNotificationOnPasswordExpiry();
            Assert.True(password.ExpiryNotificationSent);
            
            password.CreatedOn = DateTime.Today.AddDays(-83);
            password.ExpiryNotificationSent = false;
            password.SendNotificationOnPasswordExpiry();
            Assert.False(password.ExpiryNotificationSent);

            password.CreatedOn = DateTime.Today.AddDays(-56);
            password.ExpiryNotificationSent = false;
            password.SendNotificationOnPasswordExpiry();
            Assert.False(password.ExpiryNotificationSent);
                      

        }
    }
}