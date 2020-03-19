using ProjectStore.Core.Data;
using ProjectStore.Pagamentos.Business.Entities;

namespace ProjectStore.Pagamentos.Business.Interfaces
{
    public interface IPagamentoRepository : IRepository<Pagamento>
    {
        void Adicionar(Pagamento pagamento);

        void AdicionarTransacao(Transacao transacao);
    }
}