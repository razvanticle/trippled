using TrippleD.Domain.SharedKernel;

namespace TrippleD.Domain.ProductRequests
{
    public class RequestOffer:ValueObjectBase<RequestOffer>
    {
        public RequestOffer(int price)
        {
            Price = price;
        }

        public int Price { get;  }
    }
}