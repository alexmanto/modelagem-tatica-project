using System;
using TestStore.Core.DomainObjects;

namespace TestStore.Catalogo.Domain
{
    public class Produto : Entity, IAggregateRoot
    {
        public string Nome { get; private set; }

        public string Descricao { get; private set; }

        public bool Ativo { get; private set; }

        public decimal Valor { get; private set; }

        public DateTime DataCadastro { get; private set; }

        public string Imagem { get; private set; }

        public int QuantidadeEstoque { get; private set; }

        public Categoria Categoria { get; private set; }

        public Guid CategoriaId { get; private set; }

        public Produto(string nome, string descricao, bool ativo, decimal valor, DateTime dataCadastro, string imagem, Guid categoriaId)
        {
            if (string.IsNullOrEmpty(nome))
                throw new Exception("A nome informado não pode ser nulo.");
            if (string.IsNullOrEmpty(descricao))
                throw new Exception("A descrição informada não pode ser nula.");
            if (valor <= 0)
                throw new Exception("A valor informado deve ser mais que zero.");

            Nome = nome;
            Descricao = descricao;
            Ativo = ativo;
            Valor = valor;
            DataCadastro = dataCadastro;
            Imagem = imagem;
            CategoriaId = categoriaId;
        }

        public void Ativar() => Ativo = true;

        public void Desativar() => Ativo = false;

        public void AlterarCategoria(Categoria categoria)
        {
            Categoria = categoria;
            CategoriaId = categoria.Id;
        }

        public void AlterarDescricao(string descricao)
        {
            if (string.IsNullOrEmpty(descricao))
                throw new Exception("A descrição informada não pode ser nula.");

            Descricao = descricao;
        }

        public void DebitarEstoque(int quantidade)
        {
            if (quantidade < 0)
                quantidade *= -1;

            QuantidadeEstoque -= quantidade;
        }

        public void ReporEstoque(int quantidade)
        {
            if (quantidade <= 0)
                throw new Exception("A quantidade informada para repor estoque deve ser maior que zero.");

            QuantidadeEstoque += quantidade;
        }

        public bool PossuiEstoqueSuficiente(int quantidade)
        {
            if (quantidade <= 0)
                throw new Exception("A quantidade informada para repor estoque deve ser maior que zero.");

            return QuantidadeEstoque >= quantidade;
        }

        public void Validar()
        {

        }
    }
}