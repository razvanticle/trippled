using TrippleD.Domain.SharedKernel;

namespace TrippleD.Domain.Company
{
    public class Service : ValueObjectBase<Service>
    {
        public Service(string name, decimal rating)
        {
            Name = name;
            Rating = rating;
        }

        public string Name { get; }

        public decimal Rating { get; }
    }
}