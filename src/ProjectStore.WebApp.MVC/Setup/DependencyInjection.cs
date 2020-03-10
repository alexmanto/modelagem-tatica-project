using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ProjectStore.Catalogo.Application.Services;
using ProjectStore.Catalogo.Data;
using ProjectStore.Catalogo.Data.Repositories;
using ProjectStore.Catalogo.Domain.Events;
using ProjectStore.Catalogo.Domain.Interfaces;
using ProjectStore.Catalogo.Domain.Services;
using ProjectStore.Core.Communication.Mediator;
using ProjectStore.Core.Messages.CommonMessages.Notifications;
using ProjectStore.Vendas.Application.Commands;
using ProjectStore.Vendas.Data;
using ProjectStore.Vendas.Data.Repository;
using ProjectStore.Vendas.Domain.Interfaces;

namespace ProjectStore.WebApp.MVC.Setup
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            // Mediator
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            // Notifications
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            // Catálogo
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IProdutoAppService, ProdutoAppService>();
            services.AddScoped<IEstoqueService, EstoqueService>();
            services.AddScoped<CatalogoContext>();

            services.AddScoped<INotificationHandler<ProdutoAbaixoEstoqueEvent>, ProdutoEventHandler>();

            // Vendas
            services.AddScoped<IPedidoRepository, PedidoRepository>();
            services.AddScoped<VendasContext>();

            services.AddScoped<IRequestHandler<AdicionarItemPedidoCommand, bool>, PedidoCommandHandler>();
        }
    }
}