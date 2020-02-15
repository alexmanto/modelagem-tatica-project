using System;
using System.Threading.Tasks;

namespace ProjectStore.Catalogo.Domain.Interfaces
{
    public interface IEstoqueService : IDisposable
    {
        Task<bool> DebitarEstoque(Guid produtoId, int quantidade);

        Task<bool> ReporEstoque(Guid produtoId, int quantidade);
    }
}