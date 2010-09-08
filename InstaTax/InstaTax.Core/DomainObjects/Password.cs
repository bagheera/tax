using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace InstaTax.Core.DomainObjects{

    public class PasswordHistory
    {
        private IList<Password> PriorPasswords { get; set; }

        public PasswordHistory()
        {
            PriorPasswords = new List<Password>();
        }

        public bool IsSameAsPriorPasswords(String newPassword)
        {
            var lastThreePriorPasswords = GetLastThreePriorPasswords();
            foreach (Password priorPassword in lastThreePriorPasswords)
            {
                if (newPassword.Equals(priorPassword.PasswordString))
                {
                    return true;
                }
            }
            return false;
        }

        private IList<Password> GetLastThreePriorPasswords()
        {

            var lastThreePriorPasswords = new List<Password>();
            int count = 0;

            foreach (Password priorPassword in PriorPasswords)
            {
                count++;
                if (count > 3) break;
                lastThreePriorPasswords.Add(priorPassword);
            }
            return lastThreePriorPasswords;
        }


        public void Add(Password priorOnePassword)
        {
            PriorPasswords.Add(priorOnePassword);
        }
    }


    public class Password{
        private const int ExpiryDuration = 90;
        private const string CapRegExpMatcher = "[A-Z]";
        private const string SmallRegExpMatcher = "[a-z]";
        private const string DigitRegExpMatcher = "[0-9]";
        private const string SpecialCharRegExpMatcher = "[^a-zA-Z0-9]";

        public String PasswordString { get; set; }
        public DateTime CreatedOn { get; set; }
        public Boolean ExpiryNotificationSent { get; set; }
        public PasswordHistory PswdHistory { get; set; }

        public Password(){
            PswdHistory = new PasswordHistory();
        }

        private bool HasRequiredLength(){
            if (String.IsNullOrEmpty(PasswordString))
                return false;
            if (PasswordString.Length < 8)
                return false;
            return true;
        }

        private bool HasAlteastOneCapitalLetter(){
            return RegExpMatcher(CapRegExpMatcher);
        }

        private bool RegExpMatcher(String searchPhrase){
            Match match = Regex.Match(PasswordString, searchPhrase);
            return match.Success;
        }

        private bool HasAlteastOneSmallLetter(){
            return RegExpMatcher(SmallRegExpMatcher);
        }

        private bool HasAlteastOneDigit(){
            return RegExpMatcher(DigitRegExpMatcher);
        }

        private bool HasAlteastOneSpecialCharacter(){
            return RegExpMatcher(SpecialCharRegExpMatcher);
        }

        public bool IsValidPassword(){
            if (!HasRequiredLength())
                return false;

            int pwStrength = GetPasswordStrength();

            if (pwStrength < 3)
                return false;
            if (PswdHistory.IsSameAsPriorPasswords(PasswordString))
                return false;

            return true;
        }

        private int GetPasswordStrength(){
            var pwStrength = 0;
            if (HasAlteastOneCapitalLetter())
                pwStrength++;
            if (HasAlteastOneSmallLetter())
                pwStrength++;
            if (HasAlteastOneDigit())
                pwStrength++;
            if (HasAlteastOneSpecialCharacter())
                pwStrength++;
            return pwStrength;
        }

        public bool IsExpired(){
            if (DateTime.Now.Subtract(CreatedOn).Days > ExpiryDuration){
                return true;
            }
            return false;
        }

        public void SendNotificationOnPasswordExpiry(){
            if (IsDueForExpiry() && !ExpiryNotificationSent){
                Console.WriteLine("Reminder Email for password expiry was send");
                ExpiryNotificationSent = true;
            }
        }

        private bool IsDueForExpiry(){
            if (DateTime.Now.Subtract(CreatedOn).Days > ExpiryDuration - 7){
                return true;
            }
            return false;
        }
    }
}