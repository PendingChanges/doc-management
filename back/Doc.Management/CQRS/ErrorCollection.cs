using System.Collections.Generic;

namespace Doc.Management.CQRS
{
    public class ErrorCollection
    {
        public static class WellKnownErrors
        {

            internal static readonly Dictionary<string, string> Messages = new()
            {

            };
        }

        private readonly List<DomainError> _errors = new();

        public bool HasErrors => _errors.Count > 0;

        public void Add(DomainError error) => _errors.Add(error);

        public void AddError(string code) => Add(new DomainError(code,
            WellKnownErrors.Messages.TryGetValue(code, out var message) ? message : "Unknown Message"));

        public IEnumerable<DomainError> GetErrors() => _errors;
    }
}
