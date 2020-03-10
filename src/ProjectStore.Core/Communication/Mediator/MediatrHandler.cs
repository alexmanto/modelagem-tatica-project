using MediatR;
using ProjectStore.Core.Messages;
using ProjectStore.Core.Messages.CommonMessages.Notifications;
using System.Threading.Tasks;

namespace ProjectStore.Core.Communication.Mediator
{
    public class MediatorHandler : IMediatorHandler
    {
        public readonly IMediator _mediator;

        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task PublishEvent<T>(T eventParam) where T : Event
        {
            await _mediator.Publish(eventParam);
        }

        public async Task<bool> SendCommand<T>(T command) where T : Command
        {
            return await _mediator.Send(command);
        }

        public async Task PublishNotification<T>(T notification) where T : DomainNotification
        {
            await _mediator.Publish(notification);
        }
    }
}