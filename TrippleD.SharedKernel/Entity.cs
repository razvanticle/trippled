using System;
using TrippleD.SharedKernel.Identities;

namespace TrippleD.SharedKernel
{
    public abstract class Entity : IEquatable<Entity>
    {
        protected Entity(IIdentity id)
        {
            if (Equals(id, default(IIdentity)))
            {
                throw new ArgumentException("The ID cannot be the type's default value.", nameof(id));
            }

            Id = id;
        }
        
        public IIdentity Id { get; }

        public override bool Equals(object otherObject)
        {
            Entity entity = otherObject as Entity;
            if (entity != null)
            {
                return Equals(entity);
            }
            return base.Equals(otherObject);
        }

        public bool Equals(Entity other)
        {
            return other != null && Id.Equals(other.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}