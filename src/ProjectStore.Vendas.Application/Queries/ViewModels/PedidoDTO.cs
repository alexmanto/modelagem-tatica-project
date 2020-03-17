using System;

namespace ProjectStore.Vendas.Application.Queries.ViewModels
{
    public class PedidoDTO
    {
        public int Codigo { get; set; }
        public decimal ValorTotal { get; set; }
        public DateTime DataCadastro { get; set; }
        public int PedidoStatus { get; set; }
    }
}