using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using TestStore.Catalogo.Domain;
using TestStore.Core.DomainObjects;
using Xunit;

namespace ProjectStore.Catalogo.Domain.Tests
{
    public class ProdutoTests
    {
        private static readonly string NOME = "Nome";
        private static readonly string DESCRICAO = "Descrição";
        private static readonly DateTime DATA_CADASTRO = DateTime.Now;
        private static readonly string IMAGEM = "Imagem";
        private static readonly Guid CATEGORIA_ID = Guid.NewGuid();

        public class ConstrutorProduto : ProdutoTests
        {
            [Fact]
            public void Produto_Validar_Exceptions()
            {

                var domainException = Assert.Throws<DomainException>(() =>
                    new Produto(string.Empty, DESCRICAO, false, 100, DATA_CADASTRO, IMAGEM, CATEGORIA_ID, new Dimensoes(1, 1, 1))
                );

                domainException = Assert.Throws<DomainException>(() =>
                    new Produto(NOME, string.Empty, false, 100, DATA_CADASTRO, IMAGEM, CATEGORIA_ID, new Dimensoes(1, 1, 1))
                );

                domainException = Assert.Throws<DomainException>(() =>
                    new Produto(NOME, DESCRICAO, false, 0, DATA_CADASTRO, IMAGEM, CATEGORIA_ID, new Dimensoes(1, 1, 1))
                );

                domainException = Assert.Throws<DomainException>(() =>
                    new Produto(NOME, DESCRICAO, false, -5, DATA_CADASTRO, IMAGEM, CATEGORIA_ID, new Dimensoes(1, 1, 1))
                );

                var produto = new Produto(NOME, DESCRICAO, false, 100, DATA_CADASTRO, string.Empty, CATEGORIA_ID, new Dimensoes(1, 1, 1));
                var dataAnnotationsErrors = GetDataAnnotationsErrors(produto);
                Assert.True(dataAnnotationsErrors.Where(x => x.ErrorMessage.Contains("A image não pode ser nula ou vazia.")).Count() > 0);
                Assert.Equal(nameof(produto.Imagem), dataAnnotationsErrors.Select(x => x.MemberNames.FirstOrDefault()).FirstOrDefault().ToString());

                produto = new Produto(NOME, DESCRICAO, false, 100, DATA_CADASTRO, IMAGEM, Guid.Empty, new Dimensoes(1, 1, 1));
                dataAnnotationsErrors = GetDataAnnotationsErrors(produto);
                Assert.True(dataAnnotationsErrors.Where(x => x.ErrorMessage.Contains("A categoria não pode ser nula ou vazia.")).Count() > 0);
                Assert.Equal(nameof(produto.CategoriaId), dataAnnotationsErrors.Select(x => x.MemberNames.FirstOrDefault()).FirstOrDefault().ToString());
            }

            [Fact]
            public void Produto_Validar_Entidade_Instanciada()
            {
                var produto = new Produto(NOME, DESCRICAO, false, 100, DATA_CADASTRO, IMAGEM, CATEGORIA_ID, new Dimensoes(1, 1, 1));

                Assert.Equal(NOME, produto.Nome);
                Assert.Equal(DESCRICAO, produto.Descricao);
                Assert.Equal(DATA_CADASTRO, produto.DataCadastro);
                Assert.Equal(IMAGEM, produto.Imagem);
                Assert.Equal(CATEGORIA_ID, produto.CategoriaId);
                Assert.NotNull(produto.Dimensoes);
                Assert.False(produto.Ativo);
            }
        }

        public class Ativar
        {
            [Fact]
            public void Validar_Situacao_Instancia_Ativo_True()
            {
                var produto = new Produto(NOME, DESCRICAO, true, 100, DATA_CADASTRO, IMAGEM, CATEGORIA_ID, new Dimensoes(1, 1, 1));
                Assert.True(produto.Ativo);
                produto.Ativar();
                Assert.True(produto.Ativo);
            }

            [Fact]
            public void Validar_Situacao_Instancia_Ativo_False()
            {
                var produto = new Produto(NOME, DESCRICAO, false, 100, DATA_CADASTRO, IMAGEM, CATEGORIA_ID, new Dimensoes(1, 1, 1));
                Assert.False(produto.Ativo);
                produto.Ativar();
                Assert.True(produto.Ativo);
            }
        }

        public class Desativar
        {
            [Fact]
            public void Validar_Situacao_Instancia_Ativo_True()
            {
                var produto = new Produto(NOME, DESCRICAO, true, 100, DATA_CADASTRO, IMAGEM, CATEGORIA_ID, new Dimensoes(1, 1, 1));
                Assert.True(produto.Ativo);
                produto.Desativar();
                Assert.False(produto.Ativo);
            }

            [Fact]
            public void Validar_Situacao_Instancia_Ativo_False()
            {
                var produto = new Produto(NOME, DESCRICAO, false, 100, DATA_CADASTRO, IMAGEM, CATEGORIA_ID, new Dimensoes(1, 1, 1));
                Assert.False(produto.Ativo);
                produto.Desativar();
                Assert.False(produto.Ativo);
            }
        }

        public class AlterarCategoria
        {

        }

        public class AlterarDescricao
        {
            [Fact]
            public void Produto_Validar_Exceptions()
            {
                var produto = new Produto(NOME, DESCRICAO, false, 100, DATA_CADASTRO, IMAGEM, CATEGORIA_ID, new Dimensoes(1, 1, 1));
                Assert.Equal(DESCRICAO, produto.Descricao);

                var domainException = Assert.Throws<DomainException>(() =>
                    produto.AlterarDescricao(string.Empty)
                );
            }

            [Fact]
            public void Validar_Alteracao_Descricao()
            {
                var produto = new Produto(NOME, DESCRICAO, false, 100, DATA_CADASTRO, IMAGEM, CATEGORIA_ID, new Dimensoes(1, 1, 1));
                Assert.Equal(DESCRICAO, produto.Descricao);
                produto.AlterarDescricao("Nova descrição");
                Assert.NotEqual(DESCRICAO, produto.Descricao);
                Assert.Equal("Nova descrição", produto.Descricao);
            }
        }

        public class DebitarEstoque
        {

        }

        public class ReporEstoque
        {

        }

        public class PossuiEstoqueSuficiente
        {

        }

        private IList<ValidationResult> GetDataAnnotationsErrors(object entity)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(entity, null, null);
            Validator.TryValidateObject(entity, ctx, validationResults, true);
            return validationResults;
        }
    }
}