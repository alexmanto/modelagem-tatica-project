using ProjectStore.Core.Data;
using ProjectStore.Vendas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectStore.Vendas.Domain.Interfaces
{
    public interface IPedidoRepository : IRepository<Pedido>
    {
        Task<Pedido> GetById(Guid id);
        Task<IEnumerable<Pedido>> GetListaByClienteId(Guid clienteId);
        Task<Pedido> GetPedidoRascunhoByClienteId(Guid clienteId);
        void Add(Pedido pedido);
        void Update(Pedido pedido);
        Task<PedidoItem> GetItemById(Guid id);
        Task<PedidoItem> GetItemByPedido(Guid pedidoId, Guid produtoId);
        void AddItem(PedidoItem pedidoItem);
        void UpdateItem(PedidoItem pedidoItem);
        void RemoveItem(PedidoItem pedidoItem);
        Task<Voucher> GetVoucherByCodigo(string codigo);
    }
}