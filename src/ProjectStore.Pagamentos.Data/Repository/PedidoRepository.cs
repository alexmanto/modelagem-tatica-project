using ProjectStore.Core.Data;
using ProjectStore.Pagamentos.Business.Entities;
using ProjectStore.Pagamentos.Business.Interfaces;

namespace ProjectStore.Pagamentos.Data.Repository
{
    public class PagamentoRepository : IPagamentoRepository
    {
        private readonly PagamentoContext _context;

        public PagamentoRepository(PagamentoContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;


        public void Adicionar(Pagamento pagamento)
        {
            _context.Pagamentos.Add(pagamento);
        }

        public void AdicionarTransacao(Transacao transacao)
        {
            _context.Transacoes.Add(transacao);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}