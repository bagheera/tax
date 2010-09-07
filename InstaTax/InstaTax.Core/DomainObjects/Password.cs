using System;
using System.Text.RegularExpressions;

namespace InstaTax.Core.DomainObjects
{
    public class Password{
        private const string CapRegExpMatcher = "[A-Z]";
        private const string SmallRegExpMatcher = "[a-z]";
        private const string DigitRegExpMatcher = "[0-9]";
        private const string SpecialCharRegExpMatcher = "[^a-zA-Z0-9]";

        private String _passwordString;
        
        public string PasswordString{
           set { _passwordString = value; }
        }

        private bool HasRequiredLength()
        {
            if(String.IsNullOrEmpty(_passwordString))
                return false;
            if (_passwordString.Length < 8)
                return false;
            return true;
            
        }

        private bool HasAlteastOneCapitalLetter(){
            return RegExpMatcher(CapRegExpMatcher);
        }

        private bool RegExpMatcher(String searchPhrase){

           Match match = Regex.Match(_passwordString, searchPhrase);

            return match.Success;
        }

        private bool HasAlteastOneSmallLetter()
        {
            return RegExpMatcher(SmallRegExpMatcher);
        }

        private bool HasAlteastOneDigit()
        {
            return RegExpMatcher(DigitRegExpMatcher);
        }

        private bool HasAlteastOneSpecialCharacter()
        {
            return RegExpMatcher(SpecialCharRegExpMatcher);
        }

        public bool IsValidPassword(){

            if(!HasRequiredLength())
                return false;

            int pwStrength = GetPasswordStrength();

            if(pwStrength < 3)
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

        public bool isExpired(){
            throw new NotImplementedException();
        }
    }
}

