using MediatR;
using ProjectStore.Core.Messages;
using System.Threading.Tasks;

namespace ProjectStore.Core.MediatorBus
{
    public class MediatorHandler : IMediatorHandler
    {
        public readonly IMediator _mediator;

        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task PublishEvent<T>(T evento) where T : Event
        {
            await _mediator.Publish(evento);
        }

        public async Task<bool> SendCommand<T>(T comando) where T : Command
        {
            return await _mediator.Send(comando);
        }

        //public async Task PublishNotification<T>(T notificacao) where T : DomainNotification
        //{
        //    await _mediator.Publish(notificacao);
        //}
    }
}