using System;

namespace TrippleD.SharedKernel.Identities
{
    public class Identity : IEquatable<Identity>, IIdentity
    {
        public Identity(Guid value)
        {
            Value = value;
        }

        public Identity()
        {
            Value = Guid.NewGuid();
        }

        public Guid Value { get; }

        public bool Equals(Identity id)
        {
            if (ReferenceEquals(this, id))
            {
                return true;
            }

            if (ReferenceEquals(null, id))
            {
                return false;
            }

            return Value.Equals(id.Value);
        }

        public override bool Equals(object anotherObject)
        {
            return Equals(anotherObject as Identity);
        }

        public override int GetHashCode()
        {
            return (GetType().GetHashCode() * 907) + Value.GetHashCode();
        }

        public override string ToString()
        {
            return GetType().Name + " [Id=" + Value + "]";
        }

        public static IIdentity Create()
        {
            return new Identity();
        }

        public static IIdentity Create(Guid guid)
        {
            return new Identity(guid);
        }
    }
}