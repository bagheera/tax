using System.Collections.Generic;
using InstaTax.Core.DomainObjects;

namespace InstaTax.Core
{
    public interface IRepository {
        void Save(User user);
        void Save(TaxStatement taxStatement);
        User LoadByEmailId(EmailAddress emailAddress);
        List<T> LoadAll<T>();
    }
}