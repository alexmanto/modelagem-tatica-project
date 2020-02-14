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
            [Fact]
            public void Validar_Alteracao_Categoria()
            {
                var produto = new Produto(NOME, DESCRICAO, true, 100, DATA_CADASTRO, IMAGEM, CATEGORIA_ID, new Dimensoes(1, 1, 1));
                var categoria = new Categoria(NOME, 2);

                Assert.NotEqual(CATEGORIA_ID, categoria.Id);

                produto.AlterarCategoria(categoria);

                Assert.Equal(NOME, produto.Categoria.Nome);
                Assert.Equal(2, produto.Categoria.Codigo);
                Assert.Equal(categoria.Id, produto.Categoria.Id);
            }
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
            [Fact]
            public void Validar_Debito_Estoque_Quantidade_Negativa()
            {
                var produto = new Produto(NOME, DESCRICAO, true, 100, DATA_CADASTRO, IMAGEM, CATEGORIA_ID, new Dimensoes(1, 1, 1));
                produto.ReporEstoque(10);
                produto.DebitarEstoque(4);
                Assert.Equal(6, produto.QuantidadeEstoque);
                produto.DebitarEstoque(-2);
                Assert.Equal(4, produto.QuantidadeEstoque);
            }

            [Fact]
            public void Validar_Debito_Estoque()
            {
                var produto = new Produto(NOME, DESCRICAO, true, 100, DATA_CADASTRO, IMAGEM, CATEGORIA_ID, new Dimensoes(1, 1, 1));
                produto.ReporEstoque(10);
                produto.DebitarEstoque(3);
                Assert.Equal(7, produto.QuantidadeEstoque);
                produto.DebitarEstoque(5);
                Assert.Equal(2, produto.QuantidadeEstoque);
            }
        }

        public class ReporEstoque
        {
            [Fact]
            public void Produto_Validar_Exceptions_Quantidade_Negativa()
            {
                var produto = new Produto(NOME, DESCRICAO, true, 100, DATA_CADASTRO, IMAGEM, CATEGORIA_ID, new Dimensoes(1, 1, 1));
                produto.ReporEstoque(5);

                var domainException = Assert.Throws<DomainException>(() =>
                    produto.ReporEstoque(-2)
                );

                domainException = Assert.Throws<DomainException>(() =>
                    produto.ReporEstoque(0)
                );
            }

            [Fact]
            public void Validar_Reposicao_Estoque()
            {
                var produto = new Produto(NOME, DESCRICAO, true, 100, DATA_CADASTRO, IMAGEM, CATEGORIA_ID, new Dimensoes(1, 1, 1));
                produto.ReporEstoque(5);
                Assert.Equal(5, produto.QuantidadeEstoque);
                produto.ReporEstoque(3);
                Assert.Equal(8, produto.QuantidadeEstoque);
            }
        }

        public class PossuiEstoqueSuficiente
        {
            [Fact]
            public void Produto_Validar_Exceptions_Quantidade_Negativa()
            {
                var produto = new Produto(NOME, DESCRICAO, true, 100, DATA_CADASTRO, IMAGEM, CATEGORIA_ID, new Dimensoes(1, 1, 1));
                produto.ReporEstoque(5);

                var domainException = Assert.Throws<DomainException>(() =>
                    produto.PossuiEstoqueSuficiente(-2)
                );

                domainException = Assert.Throws<DomainException>(() =>
                    produto.PossuiEstoqueSuficiente(0)
                );
            }

            [Fact]
            public void Validar_Possui_Estoque_Suficiente()
            {
                var produto = new Produto(NOME, DESCRICAO, true, 100, DATA_CADASTRO, IMAGEM, CATEGORIA_ID, new Dimensoes(1, 1, 1));
                produto.ReporEstoque(5);
                var ret = produto.PossuiEstoqueSuficiente(2);
                Assert.True(ret);
                ret = produto.PossuiEstoqueSuficiente(8);
                Assert.False(ret);
                ret = produto.PossuiEstoqueSuficiente(5);
                Assert.True(ret);
            }
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