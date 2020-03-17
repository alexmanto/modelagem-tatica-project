using ProjectStore.Vendas.Application.Queries.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectStore.Vendas.Application.Queries
{
    public interface IPedidoQueries
    {
        Task<CarrinhoDTO> GetCarrinhoCliente(Guid clienteId);
        Task<IEnumerable<PedidoDTO>> GetPedidosCliente(Guid clienteId);
    }
}