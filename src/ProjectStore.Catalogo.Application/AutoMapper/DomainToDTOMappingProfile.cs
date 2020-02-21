using AutoMapper;
using ProjectStore.Catalogo.Application.ViewModels;
using TestStore.Catalogo.Domain;

namespace ProjectStore.Catalogo.Application.AutoMapper
{
    public class DomainToDTOMappingProfile : Profile
    {
        public DomainToDTOMappingProfile()
        {
            CreateMap<Produto, ProdutoDTO>();

            CreateMap<Categoria, CategoriaDTO>();
        }
    }
}