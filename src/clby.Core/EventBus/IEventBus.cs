/*
 * Microsoft.eShopOnContainers.BuildingBlocks.EventBus
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace clby.Core.EventBus
{
    public interface IEventBus
    {
        void Subscribe<T>(IIntegrationEventHandler<T> handler) where T : IntegrationEvent;
        void Unsubscribe<T>(IIntegrationEventHandler<T> handler) where T : IntegrationEvent;
        void Publish(IntegrationEvent @event);
    }
}
