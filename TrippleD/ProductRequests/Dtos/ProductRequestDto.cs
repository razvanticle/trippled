using System;

namespace TrippleD.ProductRequests.Dtos
{
    public class ProductRequestDto
    {
        public Guid ProductId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}