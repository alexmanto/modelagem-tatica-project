using MediatR;
using ProjectStore.Core.Communication.Mediator;
using ProjectStore.Core.Messages;
using ProjectStore.Core.Messages.CommonMessages.Notifications;
using ProjectStore.Vendas.Application.Events;
using ProjectStore.Vendas.Domain.Entities;
using ProjectStore.Vendas.Domain.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectStore.Vendas.Application.Commands
{
    public class PedidoCommandHandler :
        IRequestHandler<AdicionarItemPedidoCommand, bool>,
        IRequestHandler<AtualizarItemPedidoCommand, bool>,
        IRequestHandler<RemoverItemPedidoCommand, bool>,
        IRequestHandler<AplicarVoucherPedidoCommand, bool>
        //IRequestHandler<IniciarPedidoCommand, bool>,
        //IRequestHandler<FinalizarPedidoCommand, bool>,
        //IRequestHandler<CancelarProcessamentoPedidoEstornarEstoqueCommand, bool>,
        //IRequestHandler<CancelarProcessamentoPedidoCommand, bool>

    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IMediatorHandler _mediatorHandler;

        public PedidoCommandHandler(IPedidoRepository pedidoRepository,
                                    IMediatorHandler mediatorHandler)
        {
            _pedidoRepository = pedidoRepository;
            _mediatorHandler = mediatorHandler;
        }

        public async Task<bool> Handle(AdicionarItemPedidoCommand message, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(message)) return false;

            var pedido = await _pedidoRepository.GetPedidoRascunhoByClienteId(message.ClienteId);
            var pedidoItem = new PedidoItem(message.ProdutoId, message.Nome, message.Quantidade, message.ValorUnitario);

            if (pedido == null)
            {
                pedido = Pedido.PedidoFactory.NewPedidoRascunho(message.ClienteId);
                pedido.AddItem(pedidoItem);

                _pedidoRepository.Add(pedido);
                pedido.AddEvent(new PedidoRascunhoIniciadoEvent(message.ClienteId, message.ProdutoId));
            }
            else
            {
                var pedidoItemExistente = pedido.PedidoItemExistente(pedidoItem);
                pedido.AddItem(pedidoItem);

                if (pedidoItemExistente)
                    _pedidoRepository.UpdateItem(pedido.PedidoItems.FirstOrDefault(p => p.ProdutoId == pedidoItem.ProdutoId));
                else
                    _pedidoRepository.AddItem(pedidoItem);

                pedido.AddEvent(new PedidoAtualizadoEvent(pedido.ClienteId, pedido.Id, pedido.ValorTotal));
            }

            pedido.AddEvent(new PedidoItemAdicionadoEvent(pedido.ClienteId, pedido.Id, message.ProdutoId, message.Nome, message.ValorUnitario, message.Quantidade));
            return await _pedidoRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(AtualizarItemPedidoCommand message, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(message)) return false;

            var pedido = await _pedidoRepository.GetPedidoRascunhoByClienteId(message.ClienteId);

            if (pedido == null)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("pedido", "Pedido não encontrado."));
                return false;
            }

            var pedidoItem = await _pedidoRepository.GetItemByPedido(pedido.Id, message.ProdutoId);

            if (!pedido.PedidoItemExistente(pedidoItem))
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("pedido", "Item do pedido não encontrado."));
                return false;
            }

            pedido.UpdateUnidades(pedidoItem, message.Quantidade);

            pedido.AddEvent(new PedidoAtualizadoEvent(pedido.ClienteId, pedido.Id, pedido.ValorTotal));
            pedido.AddEvent(new PedidoProdutoAtualizadoEvent(message.ClienteId, pedido.Id, message.ProdutoId, message.Quantidade));

            _pedidoRepository.UpdateItem(pedidoItem);
            _pedidoRepository.Update(pedido);

            return await _pedidoRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(RemoverItemPedidoCommand message, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(message)) return false;

            var pedido = await _pedidoRepository.GetPedidoRascunhoByClienteId(message.ClienteId);

            if (pedido == null)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("pedido", "Pedido não encontrado!"));
                return false;
            }

            var pedidoItem = await _pedidoRepository.GetItemByPedido(pedido.Id, message.ProdutoId);

            if (pedidoItem != null && !pedido.PedidoItemExistente(pedidoItem))
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("pedido", "Item do pedido não encontrado!"));
                return false;
            }

            pedido.RemoveItem(pedidoItem);
            pedido.AddEvent(new PedidoAtualizadoEvent(pedido.ClienteId, pedido.Id, pedido.ValorTotal));
            pedido.AddEvent(new PedidoProdutoRemovidoEvent(message.ClienteId, pedido.Id, message.ProdutoId));

            _pedidoRepository.RemoveItem(pedidoItem);
            _pedidoRepository.Update(pedido);

            return await _pedidoRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(AplicarVoucherPedidoCommand message, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(message)) return false;

            var pedido = await _pedidoRepository.GetPedidoRascunhoByClienteId(message.ClienteId);

            if (pedido == null)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("pedido", "Pedido não encontrado!"));
                return false;
            }

            var voucher = await _pedidoRepository.GetVoucherByCodigo(message.CodigoVoucher);

            if (voucher == null)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("pedido", "Voucher não encontrado!"));
                return false;
            }

            var voucherAplicacaoValidation = pedido.AplicarVoucher(voucher);
            if (!voucherAplicacaoValidation.IsValid)
            {
                foreach (var error in voucherAplicacaoValidation.Errors)
                {
                    await _mediatorHandler.PublishNotification(new DomainNotification(error.ErrorCode, error.ErrorMessage));
                }

                return false;
            }

            pedido.AddEvent(new PedidoAtualizadoEvent(pedido.ClienteId, pedido.Id, pedido.ValorTotal));
            pedido.AddEvent(new VoucherAplicadoPedidoEvent(message.ClienteId, pedido.Id, voucher.Id));

            _pedidoRepository.Update(pedido);

            return await _pedidoRepository.UnitOfWork.Commit();
        }

        private bool ValidateCommand(Command message)
        {
            if (message.IsValid()) return true;

            foreach (var error in message.ValidationResult.Errors)
            {
                _mediatorHandler.PublishNotification(new DomainNotification(message.MessageType, error.ErrorMessage));
            }

            return false;
        }
    }
}