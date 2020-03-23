using ProjectStore.Core.DomainObjects;
using ProjectStore.Pagamentos.Business.Enums;
using System;

namespace ProjectStore.Pagamentos.Business.Entities
{
    public class Transacao : Entity
    {
        public Guid PedidoId { get; set; }
        public Guid PagamentoId { get; set; }
        public decimal Total { get; set; }
        public StatusTransacao StatusTransacao { get; set; }

        // EF. Rel.
        public Pagamento Pagamento { get; set; }
    }
}