using ProjectStore.Core.Messages;
using System.Threading.Tasks;

namespace ProjectStore.Core.MediatorBus
{
    public interface IMediatorHandler
    {
        Task PublishEvent<T>(T evento) where T : Event;
        Task<bool> SendCommand<T>(T comando) where T : Command;
        //Task PublicarNotificacao<T>(T notificacao) where T : DomainNotification;
    }
}