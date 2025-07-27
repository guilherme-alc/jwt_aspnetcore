namespace Login.Core.Context.SharedContext.Entities
{
    public abstract class Entity : IEquatable<Guid>
    {
        public Guid Id { get; } = Guid.NewGuid();
        public bool Equals(Guid other)
        {
            return Id.Equals(other);
        }
        public override bool Equals(object? obj)
        {
            if (obj is Entity entity)
            {
                return Id.Equals(entity.Id);
            }
            return false;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}   
