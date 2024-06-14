using System.Collections.Generic;

namespace Doc.Management.CQRS
{
    public class EventCollection
    {
        private readonly List<object> _events = new();

        public void Add(object @event) => _events.Add(@event);

        public IEnumerable<object> GetEvents() => _events;
    }
}
