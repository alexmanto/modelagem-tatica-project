using ProjectStore.Catalogo.Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace ProjectStore.Catalogo.Domain.Services
{
    public class EstoqueService : IEstoqueService
    {
        private readonly IProdutoRepository _produtoRepository;

        public EstoqueService(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task<bool> DebitarEstoque(Guid produtoId, int quantidade)
        {
            var produto = await _produtoRepository.GetById(produtoId);
            if (produto == null) return false;
            if (!produto.PossuiEstoqueSuficiente(quantidade)) return false;

            produto.DebitarEstoque(quantidade);
            _produtoRepository.Update(produto);
            return await _produtoRepository.UnitOfWork.Commit();
        }

        public async Task<bool> ReporEstoque(Guid produtoId, int quantidade)
        {
            var produto = await _produtoRepository.GetById(produtoId);
            if (produto == null) return false;

            produto.ReporEstoque(quantidade);
            _produtoRepository.Update(produto);
            return await _produtoRepository.UnitOfWork.Commit();
        }

        public void Dispose()
        {
            _produtoRepository.Dispose();
        }
    }
} 