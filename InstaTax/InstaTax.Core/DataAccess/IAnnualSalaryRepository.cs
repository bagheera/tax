using InstaTax.Core.DomainObjects;

namespace InstaTax.Core.DataAccess
{
    public interface IAnnualSalaryRepository
    {
        void SaveAnnualSalary(AnnualSalary salary);
        AnnualSalary GetAnnualSalary(User user);
    }
}