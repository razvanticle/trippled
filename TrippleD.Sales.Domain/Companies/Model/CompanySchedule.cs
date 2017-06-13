using TrippleD.SharedKernel;
using TrippleD.SharedKernel.Identities;

namespace TrippleD.Sales.Domain.Companies.Model
{
    public class CompanySchedule:AggregateRoot
    {
        public IIdentity CompanyId { get; }

        public CompanySchedule(IIdentity id, IIdentity companyId) : base(id)
        {
            CompanyId = companyId;
        }



    }


}