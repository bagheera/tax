using System.Collections.Generic;
using InstaTax.Core.DomainObjects;

namespace InstaTax.Core
{
    public interface IUserRepository {
        void Save(User user);
        User LoadByEmailId(EmailAddress emailAddress);
    }
}