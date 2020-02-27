using MediatR;
using ProjectStore.Core.Messages;
using System.Threading.Tasks;

namespace ProjectStore.Core.MediatorBus
{
    public class MediatrHandler : IMediatrHandler
    {
        public readonly IMediator _mediator;

        public MediatrHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task PublishEvent<T>(T evento) where T : Event
        {
            await _mediator.Publish(evento);
        }
    }
}