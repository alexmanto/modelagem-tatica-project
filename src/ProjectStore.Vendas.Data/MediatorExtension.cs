using ProjectStore.Core.Communication.Mediator;
using ProjectStore.Core.DomainObjects;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectStore.Vendas.Data
{
    public static class MediatorExtension
    {
        public static async Task PublishEvents(this IMediatorHandler mediator, VendasContext ctx)
        {
            var domainEntities = ctx.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.Notifications != null && x.Entity.Notifications.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.Notifications)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearEvents());

            var tasks = domainEvents
                .Select(async (domainEvent) => {
                    await mediator.PublishEvent(domainEvent);
                });

            await Task.WhenAll(tasks);
        }
    }
}