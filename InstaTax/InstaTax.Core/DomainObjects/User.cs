namespace InstaTax.Core.DomainObjects
{
    public enum Gender
    {
        Male, Female
    }

    public class User{
        public virtual ITaxExemptable HousingLoanInterest { get; set; }
        public virtual double RentPaid { get; set; }
        public virtual bool FromMetro { get; set; }
        public virtual IUserRepository Repository { get; set; }
        public virtual EmailAddress EmailAddress { get; set; }
        public virtual Password Password { get; set; }
        public virtual string Id { get; set; }
        public virtual Gender Gender { get; set; }

        public User()
        {

        }

        public User(double rentPaid, bool fromMetro, Gender gender){
            RentPaid = rentPaid;
            FromMetro = fromMetro;
            Gender = gender;
        }

        public User(EmailAddress emailId, Password password, IUserRepository repository)
        {
            Repository = repository;
            EmailAddress = emailId;
            Password = password;
        }

        public User(EmailAddress emailId, Password password)
        {
            EmailAddress = emailId;
            Password = password;
        }

        public virtual void Register()
        {
            if (CheckIfUnique())
            {
                Repository.Save(this);
            }
            else
            {
                throw new DuplicateUserException("Unique user not found");
            }
        }

        private bool CheckIfUnique()
        {
            return Repository.LoadByEmailId(EmailAddress) == null;
        }

       
        public virtual bool IsFemale()
        {
            return Gender == Gender.Female;
        }
    }
}