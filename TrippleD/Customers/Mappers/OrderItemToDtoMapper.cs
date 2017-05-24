﻿using TrippleD.Core;
using TrippleD.Core.Extensions;
using TrippleD.Core.Mappers;
using TrippleD.Customers.Dtos;
using TrippleD.Domain.Company.Model;
using TrippleD.Domain.Customers.Model;
using TrippleD.Domain.SharedKernel.Model;
using TrippleD.SharedKernel.Dtos;

namespace TrippleD.Customers.Mappers
{
    public class OrderItemToDtoMapper : IMapper<OrderItem, OrderItemDto>, IMapper<OrderItemDto, OrderItem>
    {
        private readonly IMapper mapper;

        public OrderItemToDtoMapper(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public OrderItemDto Map(OrderItem orderItem)
        {
            Guard.ArgNotNull(orderItem, nameof(orderItem));

            return new OrderItemDto
            {
                Service = orderItem.Service.Execute(mapper.Map<Service, ServiceDto>),
                ExecutionAddress = orderItem.ExecutionAddress.Execute(mapper.Map<Address, AddressDto>)
            };
        }

        public OrderItem Map(OrderItemDto orderItemDto)
        {
            Guard.ArgNotNull(orderItemDto, nameof(orderItemDto));
            var service = orderItemDto.Service.Execute(mapper.Map<ServiceDto, Service>);
            var executionAddress = orderItemDto.ExecutionAddress.Execute(mapper.Map<AddressDto, Address>);

            return new OrderItem(service, executionAddress);
        }
    }
}