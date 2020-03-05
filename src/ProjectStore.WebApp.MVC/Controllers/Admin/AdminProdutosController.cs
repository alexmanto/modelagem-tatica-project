using Microsoft.AspNetCore.Mvc;
using ProjectStore.Catalogo.Application.Services;
using ProjectStore.Catalogo.Application.ViewModels;
using System;
using System.Threading.Tasks;

namespace ProjectStore.WebApp.MVC.Controllers.Admin
{
    public class AdminProdutosController : Controller
    {
        private readonly IProdutoAppService _produtoAppService;

        public AdminProdutosController(IProdutoAppService produtoAppService)
        {
            _produtoAppService = produtoAppService;
        }

        [HttpGet]
        [Route("admin-produtos")]
        public async Task<IActionResult> Index()
        {
            return View(await _produtoAppService.GetAll());
        }

        [Route("novo-produto")]
        public async Task<IActionResult> NovoProduto()
        {
            return View(await PopularCategorias(new ProdutoDTO()));
        }

        [Route("novo-produto")]
        [HttpPost]
        public async Task<IActionResult> NovoProduto(ProdutoDTO produtoViewModel)
        {
            if (!ModelState.IsValid) return View(await PopularCategorias(produtoViewModel));

            await _produtoAppService.AddProduto(produtoViewModel);

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("editar-produto")]
        public async Task<IActionResult> AtualizarProduto(Guid id)
        {
            return View(await PopularCategorias(await _produtoAppService.GetById(id)));
        }

        [HttpPost]
        [Route("editar-produto")]
        public async Task<IActionResult> AtualizarProduto(Guid id, ProdutoDTO produtoViewModel)
        {
            var produto = await _produtoAppService.GetById(id);
            produtoViewModel.QuantidadeEstoque = produto.QuantidadeEstoque;

            ModelState.Remove("QuantidadeEstoque");
            if (!ModelState.IsValid) return View(await PopularCategorias(produtoViewModel));

            await _produtoAppService.UpdateProduto(produtoViewModel);

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("produtos-atualizar-estoque")]
        public async Task<IActionResult> AtualizarEstoque(Guid id)
        {
            return View("Estoque", await _produtoAppService.GetById(id));
        }

        [HttpPost]
        [Route("produtos-atualizar-estoque")]
        public async Task<IActionResult> AtualizarEstoque(Guid id, int quantidade)
        {
            if (quantidade > 0)
                await _produtoAppService.ReporEstoque(id, quantidade);
            else
                await _produtoAppService.DebitarEstoque(id, quantidade);

            return View("Index", await _produtoAppService.GetAll());
        }

        private async Task<ProdutoDTO> PopularCategorias(ProdutoDTO produto)
        {
            produto.Categoria = await _produtoAppService.GetCategorias();
            return produto;
        }
    }
}