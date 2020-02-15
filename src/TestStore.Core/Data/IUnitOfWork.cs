using System.Threading.Tasks;

namespace ProjectStore.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}