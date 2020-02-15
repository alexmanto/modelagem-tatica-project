using ProjectStore.Core.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestStore.Catalogo.Domain;

namespace ProjectStore.Catalogo.Domain.Interfaces
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        Task<IEnumerable<Produto>> GetAll();

        Task<Produto> GetById(Guid id);

        Task<IEnumerable<Produto>> GetByCategoria(int id);

        Task<IEnumerable<Categoria>> GetCategorias();

        void Insert(Produto produto);

        void Update(Produto produto);

        void Insert(Categoria categoria);

        void Update(Categoria categoria);
    }
}