using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectStore.Catalogo.Application.ViewModels
{
    public class ProdutoDTO
    {
        [Key]
        public Guid Id { get; set; }

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

        public DimensoesDTO Dimensoes { get; private set; }

        public IEnumerable<CategoriaDTO> Categoria { get; private set; }

        [Required]
        [RegularExpression("^(?!(00000000-0000-0000-0000-000000000000)$)", ErrorMessage = "A categoria não pode ser nula ou vazia.")]
        public Guid CategoriaId { get; private set; }
    }
}