using TrippleD.Domain.SharedKernel;
using TrippleD.Domain.SharedKernel.Identities;

namespace TrippleD.Domain.Products
{
    public class Product : AggregateRoot
    {
        public Product(IIdentity productId, string name, string description) : this(productId, name, description, null)
        {
        }

        public Product(IIdentity productId, string name, string description, int? price) : base(productId)
        {
            Price = price;
            Description = description;
            Name = name;
        }

        public string Description { get; }

        public string Name { get; }

        public int? Price { get; }
    }
}