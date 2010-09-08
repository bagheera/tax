namespace InstaTax.Core.DomainObjects{
    public interface IUserRepository{
        bool CheckIfUnique(User user);
        void Save(User user);
    }
}