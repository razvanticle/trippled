using System.Collections.Generic;
using TrippleD.SharedKernel.Dtos;

namespace TrippleD.Companies.Dtos
{
    public class CompanyDto
    {
        public AddressDto Address { get; set; }

        public TimeIntervalDto BusinessHours { get; set; }

        public string Email { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public decimal Rating { get; set; }

        public IList<ServiceDto> Services { get; set; }

        public string WebSite { get; set; }
    }
}