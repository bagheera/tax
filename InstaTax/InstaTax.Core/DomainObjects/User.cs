using System;
using System.Text.RegularExpressions;
using InstaTax.Core.DomainObjects;

namespace InstaTax.Core{

    public enum Gender
    {
        Male, Female
    }
    public class User{
        public double RentPaid { get; set; }
        public bool? FromMetro { get; set; }
        public IUserRepository repository;
        public virtual string Id { get; set; }
        public Password Password { get; set; }
        public Gender Gender { get; set; }

        public User()
        {
        }

        public User(double rentPaid, bool? fromMetro, Gender gender){
            RentPaid = rentPaid;
            FromMetro = fromMetro;
            Gender = gender;
        }

        public User(string id, Password password, IUserRepository repository)
        {
            this.repository = repository;
            Id = id;
            Password = password;
        }

        public void Save()
        {
            if (repository.CheckIfUnique(this))
            {
                repository.Save(this);
            }
            else
            {
                throw new DuplicateUserException("Unique user not found");
            }
        }

        public bool IsValidId()
        {
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(Id))
                return (true);
            return (false);
        }

        public bool IsFemale()
        {
            return Gender == Gender.Female;
        }
    }
}