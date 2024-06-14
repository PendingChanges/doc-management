using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Doc.Management.ValueObjects
{
    public record UserId
    {
        private readonly string _value = string.Empty;

        public UserId(string value) => _value = value;
        public static implicit operator string(UserId id) => id._value;
        public static explicit operator UserId(string value) => new(value);
        public static readonly UserId Empty = new(string.Empty);
    }
}
