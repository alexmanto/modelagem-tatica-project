using ProjectStore.Core.Messages;
using ProjectStore.Core.Messages.CommonMessages.Notifications;
using System.Threading.Tasks;

namespace ProjectStore.Core.Communication.Mediator
{
    public interface IMediatorHandler
    {
        Task PublishEvent<T>(T eventParam) where T : Event;
        Task<bool> SendCommand<T>(T command) where T : Command;
        Task PublishNotification<T>(T notificacao) where T : DomainNotification;
    }
}