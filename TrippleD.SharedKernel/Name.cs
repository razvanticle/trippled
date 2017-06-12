namespace TrippleD.Domain.SharedKernel
{
    public class Name : ValueObjectBase<Name>
    {
        public Name(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public string FirstName { get; }

        public string FullName => $"{FirstName} {LastName}";

        public string LastName { get; }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}