using MediatR;
using ProjectStore.Catalogo.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectStore.Catalogo.Domain.Events
{
    public class ProdutoEventHandler : INotificationHandler<ProdutoAbaixoEstoqueEvent>
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoEventHandler(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task Handle(ProdutoAbaixoEstoqueEvent message, CancellationToken cancellationToken)
        {
            var produto = await _produtoRepository.GetById(message.AggregateId);

            // TODO: Implementação do evento do produto, por exemplo, enviar um email para compra de mais produtos.
        }
    }
}