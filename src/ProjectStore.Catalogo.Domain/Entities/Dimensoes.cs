using System.ComponentModel.DataAnnotations;

namespace ProjectStore.Catalogo.Domain
{
    public class Dimensoes
    {
        [MinLength(1, ErrorMessage = "A altura deve ser maior que zero.")]
        public decimal Altura { get; private set; }

        [MinLength(1, ErrorMessage = "A largura deve ser maior que zero.")]
        public decimal Largura { get; private set; }

        [MinLength(1, ErrorMessage = "A profundidade deve ser maior que zero.")]
        public decimal Profundidade { get; private set; }

        public Dimensoes(decimal altura, decimal largura, decimal profundidade)
        {
            Altura = altura;
            Largura = largura;
            Profundidade = profundidade;
        }

        public string DescricaoFormatada()
        {
            return $"LxAxP: {Largura} x {Altura} x {Profundidade}";
        }

        public override string ToString()
        {
            return DescricaoFormatada();
        }
    }
}