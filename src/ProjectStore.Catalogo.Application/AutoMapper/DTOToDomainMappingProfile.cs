using AutoMapper;
using ProjectStore.Catalogo.Application.ViewModels;
using ProjectStore.Catalogo.Domain;

namespace ProjectStore.Catalogo.Application.AutoMapper
{
    public class DTOToDomainMappingProfile : Profile
    {
        public DTOToDomainMappingProfile()
        {
            CreateMap<ProdutoDTO, Produto>()
                .ConstructUsing(p =>
                    new Produto(p.Nome, p.Descricao, p.Ativo,
                        p.Valor, p.DataCadastro,
                        p.Imagem, p.CategoriaId, new Dimensoes(p.Altura, p.Largura, p.Profundidade)));

            CreateMap<CategoriaDTO, Categoria>()
                .ConstructUsing(c => new Categoria(c.Nome, c.Codigo));
        }
    }
}