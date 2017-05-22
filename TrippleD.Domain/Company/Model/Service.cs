using TrippleD.Domain.SharedKernel;

namespace TrippleD.Domain.Company.Model
{
    public class Service : ValueObjectBase<Service>
    {
        public Service()
        {

        }

        public Service(string name, decimal rating)
        {
            Name = name;
            Rating = rating;
        }

        public string Name { get; private set; }

        public decimal Rating { get; private set; }

        public Service Rate()
        {
            return new Service(Name, Rating + 1);
        }
    }
}