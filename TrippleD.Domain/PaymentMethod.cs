using TrippleD.Domain.SharedKernel;

namespace TrippleD.Domain
{
    public class PaymentMethod : ValueObjectBase<PaymentMethod>
    {
        public PaymentMethod(PaymentMethodType type)
        {
            Type = type;
        }

        public PaymentMethodType Type { get; }
    }
}