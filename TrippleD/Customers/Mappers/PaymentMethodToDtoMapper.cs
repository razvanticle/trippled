using TrippleD.Core;
using TrippleD.Core.Mappers;
using TrippleD.Customers.Dtos;
using TrippleD.Domain.Customers.Model;

namespace TrippleD.Customers.Mappers
{
    public class PaymentMethodToDtoMapper : IMapper<PaymentMethod, PaymentMethodDto>,
        IMapper<PaymentMethodDto, PaymentMethod>
    {
        public PaymentMethodDto Map(PaymentMethod paymentMethod)
        {
            Guard.ArgNotNull(paymentMethod, nameof(paymentMethod));

            return new PaymentMethodDto {Type = paymentMethod.Type};
        }

        public PaymentMethod Map(PaymentMethodDto paymentMethodDto)
        {
            Guard.ArgNotNull(paymentMethodDto, nameof(paymentMethodDto));

            return new PaymentMethod(paymentMethodDto.Type);
        }
    }
}