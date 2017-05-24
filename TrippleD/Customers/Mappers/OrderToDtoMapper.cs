using System.Linq;
using TrippleD.Core;
using TrippleD.Core.Extensions;
using TrippleD.Core.Mappers;
using TrippleD.Customers.Dtos;
using TrippleD.Domain.Customers.Model;
using TrippleD.Domain.SharedKernel.Model;
using TrippleD.SharedKernel.Dtos;

namespace TrippleD.Customers.Mappers
{
    public class OrderToDtoMapper : IMapper<Order, OrderDto>, IMapper<OrderDto, Order>
    {
        private readonly IMapper mapper;

        public OrderToDtoMapper(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public OrderDto Map(Order order)
        {
            Guard.ArgNotNull(order, nameof(order));

            return new OrderDto
            {
                Id = order.Id,
                PaymentMethod = order.PaymentMethod.Execute(mapper.Map<PaymentMethod, PaymentMethodDto>),
                InvoiceAddress = order.Invoice.Address.Execute(mapper.Map<Address, AddressDto>),
                OrderItems = order.OrderItems.Select(mapper.Map<OrderItem, OrderItemDto>)
            };
        }

        public Order Map(OrderDto orderDto)
        {
            Guard.ArgNotNull(orderDto, nameof(orderDto));

            // todo move this to factory?
            var paymentMethod = orderDto.PaymentMethod.Execute(mapper.Map<PaymentMethodDto, PaymentMethod>);
            var orderItems = orderDto.OrderItems.Select(mapper.Map<OrderItemDto, OrderItem>);
            var invoiceAddress = orderDto.InvoiceAddress.Execute(mapper.Map<AddressDto, Address>);
            var invoice = new Invoice(invoiceAddress);

            return new Order(invoice, orderItems, paymentMethod);
        }
    }
}