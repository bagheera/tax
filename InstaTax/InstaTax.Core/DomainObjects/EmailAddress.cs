using System;
using System.Text.RegularExpressions;

namespace InstaTax.Core.DomainObjects
{
    public class EmailAddress
    {
        private string value;

        public EmailAddress()
        {
            // DO NOTHING
        }

        public EmailAddress(string value)
        {
            if (value==null) throw new ArgumentNullException();
            if(!IsValidId(value)) throw new ArgumentException();
            this.value = value;
        }

        private bool IsValidId(string value)
        {
            const string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                                    @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                                    @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            var re = new Regex(strRegex);
            return re.IsMatch(value);
        }

        public override string ToString()
        {
            return value;
        }

        public bool Equals(EmailAddress other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.value, value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (EmailAddress)) return false;
            return Equals((EmailAddress) obj);
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }
    }


}