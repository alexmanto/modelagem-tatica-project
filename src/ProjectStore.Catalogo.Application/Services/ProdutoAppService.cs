using AutoMapper;
using ProjectStore.Catalogo.Application.ViewModels;
using ProjectStore.Catalogo.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestStore.Catalogo.Domain;
using TestStore.Core.DomainObjects;

namespace ProjectStore.Catalogo.Application.Services
{
    public class ProdutoAppService : IProdutoAppService
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMapper _mapper;
        private readonly IEstoqueService _estoqueService;

        public ProdutoAppService(IProdutoRepository produtoRepository, IMapper mapper, IEstoqueService estoqueService)
        {
            _produtoRepository = produtoRepository;
            _mapper = mapper;
            _estoqueService = estoqueService;
        }

        public async Task<ProdutoDTO> GetById(Guid id)
        {
            return _mapper.Map<ProdutoDTO>(await _produtoRepository.GetById(id));
        }

        public async Task<IEnumerable<ProdutoDTO>> GetAll()
        {
            return _mapper.Map<IEnumerable<ProdutoDTO>>(await _produtoRepository.GetAll());
        }

        public async Task<IEnumerable<ProdutoDTO>> GetByCategoria(int codigo)
        {
            return _mapper.Map<IEnumerable<ProdutoDTO>>(await _produtoRepository.GetByCategoria(codigo));
        }

        public async Task<IEnumerable<CategoriaDTO>> GetCategorias()
        {
            return _mapper.Map<IEnumerable<CategoriaDTO>>(await _produtoRepository.GetCategorias());
        }

        public async Task AddProduto(ProdutoDTO produtoDto)
        {
            var produto = _mapper.Map<Produto>(produtoDto);
            _produtoRepository.Insert(produto);

            await _produtoRepository.UnitOfWork.Commit();
        }

        public async Task UpdateProduto(ProdutoDTO produtoDto)
        {
            var produto = _mapper.Map<Produto>(produtoDto);
            _produtoRepository.Update(produto);

            await _produtoRepository.UnitOfWork.Commit();
        }

        public async Task<ProdutoDTO> ReporEstoque(Guid id, int quantidade)
        {
            if (!_estoqueService.DebitarEstoque(id, quantidade).Result)
                throw new DomainException("Falha ao repor estoque.");

            return _mapper.Map<ProdutoDTO>(await _produtoRepository.GetById(id));
        }

        public async Task<ProdutoDTO> DebitarEstoque(Guid id, int quantidade)
        {
            if (!_estoqueService.DebitarEstoque(id, quantidade).Result)
                throw new DomainException("Falha ao debitar estoque.");

            return _mapper.Map<ProdutoDTO>(await _produtoRepository.GetById(id));
        }

        public void Dispose()
        {
            _produtoRepository?.Dispose();
            _estoqueService?.Dispose();
        }
    }
}