using TrippleD.Core;
using TrippleD.Core.Mappers;
using TrippleD.Domain.SharedKernel.Model;
using TrippleD.SharedKernel.Dtos;

namespace TrippleD.SharedKernel.Mappers
{
    public class AddressToDtoMaper : IMapper<Address, AddressDto>, IMapper<AddressDto,Address>
    {
        public AddressDto Map(Address address)
        {
            Guard.ArgNotNull(address, nameof(address));

            return new AddressDto
            {
                Street = address.Street,
                Number = address.Number,
                City = address.City
            };
        }

        public Address Map(AddressDto addressDto)
        {
            Guard.ArgNotNull(addressDto,nameof(addressDto));

            return new Address(addressDto.City, addressDto.Number, addressDto.Street);
        }
    }
}