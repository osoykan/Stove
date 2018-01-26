using System.Threading;
using System.Threading.Tasks;

using Autofac.Extras.IocManager;

namespace Stove.Events.Bus.Entities
{
    /// <summary>
    ///     Used to publish aggregate change events.
    /// </summary>
    public class AggregateChangeEventHelper : IAggregateChangeEventHelper, ITransientDependency
    {
        private readonly IEventBus _eventBus;

        public AggregateChangeEventHelper(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public virtual void PublishEvents(AggregateChangeReport aggregateChangeReport)
        {
            foreach (DomainEvent domainEvent in aggregateChangeReport.DomainEvents)
            {
                _eventBus.Publish(domainEvent.Event.GetType(), domainEvent.Event);
            }
        }

        public async Task PublishEventsAsync(AggregateChangeReport aggregateChangeReport, CancellationToken cancellationToken = default)
        {
            foreach (DomainEvent domainEvent in aggregateChangeReport.DomainEvents)
            {
                await _eventBus.PublishAsync(domainEvent.Event.GetType(), domainEvent.Event, cancellationToken);
            }
        }
    }
}
