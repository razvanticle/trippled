using TrippleD.Core;
using TrippleD.Persistence.Repository;
using TrippleD.Sales.Domain.Companies.WorkItems;
using TrippleD.Sales.Domain.ProductRequests;
using TrippleD.Sales.Domain.ProductRequests.Events;
using TrippleD.SharedKernel.Events;
using TrippleD.SharedKernel.Identities;

namespace TrippleD.Sales.Application.Companies.WorkItems
{
    [Service(typeof(IDomainEventHandler<RequestCreatedEvent>))]
    public class RequestCreatedEventHandler : IDomainEventHandler<RequestCreatedEvent>
    {
        private readonly IEntityRepository<WorkItem> workItemRepository;

        public RequestCreatedEventHandler(IEntityRepository<WorkItem> workItemRepository)
        {
            this.workItemRepository = workItemRepository;
        }

        public void Handle(RequestCreatedEvent args)
        {
            ProductRequest productRequest = args.ProductRequest;

            WorkItem workItem = new WorkItem(Identity.Create(), productRequest.ProductId, productRequest.CustomerId,
                productRequest.TimeInterval);
            workItemRepository.Add(workItem);
        }
    }
}