using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ProjectStore.Catalogo.Application.Services;
using ProjectStore.Catalogo.Data;
using ProjectStore.Catalogo.Data.Repositories;
using ProjectStore.Catalogo.Domain.Events;
using ProjectStore.Catalogo.Domain.Interfaces;
using ProjectStore.Catalogo.Domain.Services;
using ProjectStore.Core.MediatorBus;

namespace ProjectStore.WebApp.MVC.Setup
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            // Domain Bus (Mediator)
            services.AddScoped<IMediatrHandler, MediatrHandler>();

            // Catálogo
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IProdutoAppService, ProdutoAppService>();
            services.AddScoped<IEstoqueService, EstoqueService>();
            services.AddScoped<CatalogoContext>();

            services.AddScoped<INotificationHandler<ProdutoAbaixoEstoqueEvent>, ProdutoEventHandler>();
        }
    }
}