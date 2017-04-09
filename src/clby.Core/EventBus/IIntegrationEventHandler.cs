using System.Threading.Tasks;

namespace clby.Core.EventBus
{
    public interface IIntegrationEventHandler<in TIntegrationEvent> 
        : IIntegrationEventHandler
        where TIntegrationEvent : IntegrationEvent
    {
        Task Handle(TIntegrationEvent @event);
    }

    public interface IIntegrationEventHandler
    { }
}
