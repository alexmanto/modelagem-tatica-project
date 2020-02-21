using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectStore.Catalogo.Application.ViewModels
{
    public class CategoriaDTO
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "A Nome não pode ser nulo ou vazio.")]
        public string Nome { get; private set; }

        [Required(ErrorMessage = "O código não pode ser nulo ou vazio.")]
        public int Codigo { get; private set; }
    }
}