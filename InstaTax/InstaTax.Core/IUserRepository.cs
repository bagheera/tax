using System.Collections.Generic;
using InstaTax.Core.DomainObjects;

namespace InstaTax.Core
{
    public interface IUserRepository{
        bool CheckIfUnique();
        bool Save();
        IList<User> LoadByEmailId();
    }
}