using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectStore.Catalogo.Application.Services;
using ProjectStore.Core.Communication.Mediator;
using ProjectStore.Core.Messages.CommonMessages.Notifications;
using ProjectStore.Vendas.Application.Commands;
using System;
using System.Threading.Tasks;

namespace ProjectStore.WebApp.MVC.Controllers
{
    public class CarrinhoController : ControllerBase
    {
        private readonly IProdutoAppService _produtoAppService;
        //private readonly IPedidoQueries _pedidoQueries;
        private readonly IMediatorHandler _mediatorHandler;

        public CarrinhoController(INotificationHandler<DomainNotification> notifications,
                                  IProdutoAppService produtoAppService,
                                  IMediatorHandler mediatorHandler
                                  /*IPedidoQueries pedidoQueries*/) : base(notifications, mediatorHandler)
        {
            _produtoAppService = produtoAppService;
            _mediatorHandler = mediatorHandler;
            //_pedidoQueries = pedidoQueries;
        }

        [Route("meu-carrinho")]
        public async Task<IActionResult> Index()
        {
            //return View(await _pedidoQueries.ObterCarrinhoCliente(ClienteId));
            return null;
        }

        [HttpPost]
        [Route("meu-carrinho")]
        public async Task<IActionResult> AddItem(Guid id, int quantidade)
        {
            var produto = await _produtoAppService.GetById(id);
            if (produto == null) return BadRequest();

            if (produto.QuantidadeEstoque < quantidade)
            {
                TempData["Erro"] = "Produto com estoque insuficiente";
                return RedirectToAction("ProdutoDetalhe", "Vitrine", new { id });
            }

            var command = new AdicionarItemPedidoCommand(ClienteId, produto.Id, produto.Nome, quantidade, produto.Valor);
            await _mediatorHandler.SendCommand(command);

            if (OperacaoValida())
                return RedirectToAction("Index");

            //TempData["Erros"] = GetMensagensErro();
            TempData["Erros"] = "Produto indisponível";
            return RedirectToAction("ProdutoDetalhe", "Vitrine", new { id });
        }

        //[HttpPost]
        //[Route("remover-item")]
        //public async Task<IActionResult> RemoverItem(Guid id)
        //{
        //    var produto = await _produtoAppService.GetById(id);
        //    if (produto == null) return BadRequest();

        //    var command = new RemoverItemPedidoCommand(ClienteId, id);
        //    await _mediatorHandler.SendCommand(command);

        //    if (OperacaoValida())
        //    {
        //        return RedirectToAction("Index");
        //    }

        //    return View("Index", await _pedidoQueries.ObterCarrinhoCliente(ClienteId));
        //}

        //[HttpPost]
        //[Route("atualizar-item")]
        //public async Task<IActionResult> AtualizarItem(Guid id, int quantidade)
        //{
        //    var produto = await _produtoAppService.GetById(id);
        //    if (produto == null) return BadRequest();

        //    var command = new AtualizarItemPedidoCommand(ClienteId, id, quantidade);
        //    await _mediatorHandler.SendCommand(command);

        //    if (OperacaoValida())
        //    {
        //        return RedirectToAction("Index");
        //    }

        //    return View("Index", await _pedidoQueries.ObterCarrinhoCliente(ClienteId));
        //}

        //[HttpPost]
        //[Route("aplicar-voucher")]
        //public async Task<IActionResult> AplicarVoucher(string voucherCodigo)
        //{
        //    var command = new AplicarVoucherPedidoCommand(ClienteId, voucherCodigo);
        //    await _mediatorHandler.SendCommand(command);

        //    if (OperacaoValida())
        //    {
        //        return RedirectToAction("Index");
        //    }

        //    return View("Index", await _pedidoQueries.ObterCarrinhoCliente(ClienteId));
        //}

        //[Route("resumo-da-compra")]
        //public async Task<IActionResult> ResumoDaCompra()
        //{
        //    return View(await _pedidoQueries.ObterCarrinhoCliente(ClienteId));
        //}

        //[HttpPost]
        //[Route("iniciar-pedido")]
        //public async Task<IActionResult> IniciarPedido(CarrinhoViewModel carrinhoViewModel)
        //{
        //    var carrinho = await _pedidoQueries.ObterCarrinhoCliente(ClienteId);

        //    var command = new IniciarPedidoCommand(carrinho.PedidoId, ClienteId, carrinho.ValorTotal, carrinhoViewModel.Pagamento.NomeCartao,
        //        carrinhoViewModel.Pagamento.NumeroCartao, carrinhoViewModel.Pagamento.ExpiracaoCartao, carrinhoViewModel.Pagamento.CvvCartao);

        //    await _mediatorHandler.SendCommand(command);

        //    if (OperacaoValida())
        //    {
        //        return RedirectToAction("Index", "Pedido");
        //    }

        //    return View("ResumoDaCompra", await _pedidoQueries.ObterCarrinhoCliente(ClienteId));
        //}
    }
}