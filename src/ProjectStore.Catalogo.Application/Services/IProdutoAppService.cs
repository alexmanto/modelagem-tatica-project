using ProjectStore.Catalogo.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectStore.Catalogo.Application.Services
{
    public interface IProdutoAppService : IDisposable
    {
        Task<IEnumerable<ProdutoDTO>> GetByCategoria(int codigo);

        Task<ProdutoDTO> GetById(Guid id);

        Task<IEnumerable<ProdutoDTO>> GetAll();

        Task<IEnumerable<CategoriaDTO>> GetCategorias();

        Task AddProduto(ProdutoDTO produto);

        Task UpdateProduto(ProdutoDTO produto);

        Task<ProdutoDTO> DebitarEstoque(Guid id, int quantidade);

        Task<ProdutoDTO> ReporEstoque(Guid id, int quantidade);
    }
}