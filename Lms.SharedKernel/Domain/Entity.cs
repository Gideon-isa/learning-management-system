using Lms.SharedKernel.Domain.Abstractions;

namespace Lms.SharedKernel.Domain
{
    public abstract class Entity<Tkey> : IEquatable<Entity<Tkey>>
    {
        public Tkey Id { get; protected set; }
        public bool IsDeleted { get; protected set; }

        public bool Equals(Entity<Tkey>? other)
        {
            if (other is null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return false;
            }
            return Id?.GetHashCode() == other?.Id?.GetHashCode();

        }

        public override bool Equals(object? obj)
        {
            if (obj is null || obj.GetType() != GetType())
            {
                return false;
            }
            return Equals((Entity<Tkey>)obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(Entity<Tkey>? left, Entity<Tkey>? right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Entity<Tkey>? left, Entity<Tkey>? right)
        {
            return !(left.Equals(right));
        }
    }
}
