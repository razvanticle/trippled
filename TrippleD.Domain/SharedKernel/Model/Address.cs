namespace TrippleD.Domain.SharedKernel.Model
{
    public class Address : ValueObjectBase<Address>
    {
        public Address()
        {

        }

        public Address(string city, string number, string street)
        {
            City = city;
            Number = number;
            Street = street;
        }

        public string City { get; private set; }

        public string Number { get; private set; }

        public string Street { get; private set; }

        public Address SetCity(string city)
        {
            return new Address(city, Number, Street);
        }

        public Address SetNumber(string number)
        {
            return new Address(City, number, Street);
        }

        public Address SetStreet(string street)
        {
            return new Address(City, Number, street);
        }
    }
}