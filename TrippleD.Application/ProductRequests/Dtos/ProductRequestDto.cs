using System;

namespace TrippleD.Application.ProductRequests.Dtos
{
    public class ProductRequestDto
    {
        public Guid ProductId { get; set; }

        public Guid CustomerId { get; set; }

        public Guid CompanyId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}