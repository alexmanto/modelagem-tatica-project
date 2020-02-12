using System;
using System.Collections.Generic;
using System.Text;
using TestStore.Core.DomainObjects;

namespace TestStore.Catalogo.Domain
{
    public class Categoria : Entity
    {
        public string Nome { get; private set; }

        public int Codigo { get; private set; }

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