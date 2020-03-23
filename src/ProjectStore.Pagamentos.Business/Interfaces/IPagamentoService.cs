using ProjectStore.Core.DomainObjects.DTO;
using ProjectStore.Pagamentos.Business.Entities;
using System.Threading.Tasks;

namespace ProjectStore.Pagamentos.Business.Interfaces
{
    public interface IPagamentoService
    {
        Task<Transacao> RealizarPagamentoPedido(PagamentoPedido pagamentoPedido);
    }
}