using System;

namespace Doc.Management.ValueObjects
{
    public record EntityId
    {
        private readonly Guid _value;

        public EntityId(string value) : this(Guid.Parse(value)) { }
        private EntityId(Guid value) => _value = value;

        public static implicit operator string(EntityId id) => id._value.ToString();
        public static readonly EntityId Empty = new(Guid.Empty);

        public static EntityId NewEntityId() => new(Guid.NewGuid());
    }
}
