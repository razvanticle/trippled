using TrippleD.Domain.SharedKernel;

namespace TrippleD.Domain.Companies.Model
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

        public Service Rate()
        {
            return new Service(Name, Rating + 1);
        }
    }
}