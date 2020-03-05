using ProjectStore.Core.DomainObjects;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectStore.Catalogo.Domain
{
    public class Categoria : Entity
    {
        [Required(ErrorMessage = "A Nome não pode ser nulo ou vazio.")]
        public string Nome { get; private set; }

        [Required(ErrorMessage = "O código não pode ser nulo ou vazio.")]
        public int Codigo { get; private set; }

        public ICollection<Produto> Produtos { get; set; }

        protected Categoria() { }

        public Categoria(string nome, int codigo)
        {
            Nome = nome;
            Codigo = codigo;
        }

        public override string ToString()
        {
            return $"{Nome} - {Codigo}";
        }
    }
}