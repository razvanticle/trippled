namespace TrippleD.SharedKernel.Model.ProductRequest
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