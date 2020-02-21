using AutoMapper;
using ProjectStore.Catalogo.Application.ViewModels;
using TestStore.Catalogo.Domain;

namespace ProjectStore.Catalogo.Application.AutoMapper
{
    public class DTOToDomainMappingProfile : Profile
    {
        public DTOToDomainMappingProfile()
        {
            CreateMap<ProdutoDTO, Produto>()
                .ConstructUsing(p =>
                new Produto(p.Nome, p.Descricao, p.Ativo,
                    p.Valor, p.DataCadastro, p.Imagem, p.CategoriaId,
                    new Dimensoes(p.Dimensoes.Altura, p.Dimensoes.Largura, p.Dimensoes.Profundidade)));

            CreateMap<CategoriaDTO, Categoria>()
                .ConstructUsing(c => new Categoria(c.Nome, c.Codigo));
        }
    }
}