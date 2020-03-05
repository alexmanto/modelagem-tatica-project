using ProjectStore.Core.DomainObjects;
using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectStore.Catalogo.Domain
{
    public class Produto : Entity, IAggregateRoot
    {
        [Required(ErrorMessage = "O nome não pode ser nulo ou vazio.")]
        public string Nome { get; private set; }

        [Required(ErrorMessage = "A descricao não pode ser nula ou vazia.")]
        public string Descricao { get; private set; }

        public bool Ativo { get; private set; }

        [Range(1, 9999999999999999.99, ErrorMessage = "O valor deve ser mais que zero.")]
        public decimal Valor { get; private set; }

        public DateTime DataCadastro { get; private set; }

        [Required(ErrorMessage = "A image não pode ser nula ou vazia.")]
        public string Imagem { get; private set; }

        public int QuantidadeEstoque { get; private set; }

        public Dimensoes Dimensoes { get; private set; }

        public Categoria Categoria { get; private set; }

        [Required]
        [RegularExpression("^(?!(00000000-0000-0000-0000-000000000000)$)", ErrorMessage = "A categoria não pode ser nula ou vazia.")]
        public Guid CategoriaId { get; private set; }

        protected Produto()
        {

        }

        public Produto(string nome, string descricao, bool ativo, decimal valor, DateTime dataCadastro, string imagem, Guid categoriaId, Dimensoes dimensoes)
        {
            if (string.IsNullOrEmpty(nome))
                throw new DomainException($"O {nameof(Nome)} não pode ser nulo.");
            if (string.IsNullOrEmpty(descricao))
                throw new DomainException($"A {nameof(Descricao)} não pode ser nula.");
            if (valor <= 0)
                throw new DomainException($"O {nameof(Valor)} deve ser mais que zero.");

            Nome = nome;
            Descricao = descricao;
            Ativo = ativo;
            Valor = valor;
            DataCadastro = dataCadastro;
            Imagem = imagem;
            CategoriaId = categoriaId;
            Dimensoes = dimensoes;
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
                throw new DomainException($"A {nameof(Descricao)} não pode ser nula.");

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
                throw new DomainException($"A {nameof(QuantidadeEstoque)} informada para repor estoque deve ser maior que zero.");

            QuantidadeEstoque += quantidade;
        }

        public bool PossuiEstoqueSuficiente(int quantidade)
        {
            if (quantidade <= 0)
                throw new DomainException($"A {nameof(QuantidadeEstoque)} informada para repor estoque deve ser maior que zero.");

            return QuantidadeEstoque >= quantidade;
        }
    }
}