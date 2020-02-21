using System.ComponentModel.DataAnnotations;

namespace ProjectStore.Catalogo.Application.ViewModels
{
    public class DimensoesDTO
    {
        [MinLength(1, ErrorMessage = "A altura deve ser maior que zero.")]
        public decimal Altura { get; private set; }

        [MinLength(1, ErrorMessage = "A largura deve ser maior que zero.")]
        public decimal Largura { get; private set; }

        [MinLength(1, ErrorMessage = "A profundidade deve ser maior que zero.")]
        public decimal Profundidade { get; private set; }
    }
}