using System;
using System.Collections.Generic;
using System.IO;
using InstaTax.Core.DomainObjects;
using NHibernate;
using System.Linq;

namespace InstaTax.Core
{
    public class Repository : NHibernateSetup, IRepository
    {

        public Repository()
            : base(new FileInfo[]{new FileInfo("DataAccess/InstaTax.hbm.xml") })
        {
        }

        public void Save(User u)
        {
            Session.Save(u);
        }

        public void Save(TaxStatement taxStatement){
            Session.Save(taxStatement);
        }

        public User LoadByEmailId(EmailAddress emailAddress)
        {
            IQuery query = Session.CreateQuery("from User u WHERE u.EmailAddress = :emailAddress");
            query.SetParameter("emailAddress", emailAddress.ToString());

            return query.UniqueResult<User>();
        }

        public List<T> LoadAll<T>(){
            IList<T> list = Session.CreateCriteria(typeof(T)).List<T>();
            return list.ToList();
        }

        public void SaveAnnualSalary(AnnualSalary salary)
        {
            Session.Save(salary);
        }

        public AnnualSalary GetAnnualSalary(User user)
        {
            IQuery query = Session.CreateQuery("from AnnualSalary where userId = '" + user.Id + "'");
            
            IList<AnnualSalary> liSalary = query.List<AnnualSalary>();

            if (liSalary.Count > 0)
                return liSalary[0];

            return null;
        }

        public void SaveInvestment(Investment investment)
        {
            Session.Save(investment);
        }

        public Investment GetInvestmentDetails(int taxStatementId)
        {
            IQuery query = Session.CreateQuery("from  Investment ");//"where TaxStatement=: taxStatementId").SetParameter("taxStatementId", taxStatementId);

            IList<Investment> listOfInvestments = query.List<Investment>();

            return listOfInvestments[0];

        }
        
    }
   
}