﻿using ProjectStore.Catalogo.Domain.Events;
using ProjectStore.Catalogo.Domain.Interfaces;
using ProjectStore.Core.MediatorBus;
using System;
using System.Threading.Tasks;

namespace ProjectStore.Catalogo.Domain.Services
{
    public class EstoqueService : IEstoqueService
    {
        private readonly IProdutoRepository _produtoRepository;

        private readonly IMediatrHandler _bus;

        public EstoqueService(IProdutoRepository produtoRepository, IMediatrHandler bus)
        {
            _produtoRepository = produtoRepository;
            _bus = bus;
        }

        public async Task<bool> DebitarEstoque(Guid produtoId, int quantidade)
        {
            var produto = await _produtoRepository.GetById(produtoId);
            if (produto == null) return false;
            if (!produto.PossuiEstoqueSuficiente(quantidade)) return false;

            produto.DebitarEstoque(quantidade);

            //TODO: Teste de utilização do publish event
            if (produto.QuantidadeEstoque < 10)
                await _bus.PublishEvent(new ProdutoAbaixoEstoqueEvent(produto.Id, produto.QuantidadeEstoque));

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