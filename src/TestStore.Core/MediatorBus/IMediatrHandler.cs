using ProjectStore.Core.Messages;
using System.Threading.Tasks;

namespace ProjectStore.Core.MediatorBus
{
    public interface IMediatrHandler
    {
        Task PublishEvent<T>(T evento) where T : Event;
    }
}