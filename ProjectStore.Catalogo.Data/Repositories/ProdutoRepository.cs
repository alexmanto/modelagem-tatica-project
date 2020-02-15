using Microsoft.EntityFrameworkCore;
using ProjectStore.Catalogo.Domain.Interfaces;
using ProjectStore.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestStore.Catalogo.Domain;

namespace ProjectStore.Catalogo.Data.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly CatalogoContext _context;

        public IUnitOfWork UnitOfWork => _context;

        public ProdutoRepository(CatalogoContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Produto>> GetAll()
        {
            return await _context.Produtos.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Produto>> GetByCategoria(int codigo)
        {
            return await _context.Produtos.AsNoTracking().Include(p => p.Categoria).Where(c => c.Categoria.Codigo == codigo).ToListAsync();
        }

        public async Task<Produto> GetById(Guid id)
        {
            return await _context.Produtos.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Categoria>> GetCategorias()
        {
            return await _context.Categoria.AsNoTracking().ToListAsync();
        }

        public void Insert(Produto produto)
        {
            _context.Produtos.Add(produto);
        }

        public void Insert(Categoria categoria)
        {
            _context.Categoria.Add(categoria);
        }

        public void Update(Produto produto)
        {
            _context.Produtos.Update(produto);
        }

        public void Update(Categoria categoria)
        {
            _context.Categoria.Update(categoria);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}