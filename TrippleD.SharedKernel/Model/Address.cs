namespace TrippleD.SharedKernel.Model
{
    public class Address : ValueObjectBase<Address>
    {
        public Address(string city, string number, string street)
        {
            City = city;
            Number = number;
            Street = street;
        }

        public string City { get; }

        public string Number { get; }

        public string Street { get; }

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