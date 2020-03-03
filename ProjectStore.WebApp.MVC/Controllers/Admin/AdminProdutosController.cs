using Microsoft.AspNetCore.Mvc;
using ProjectStore.Catalogo.Application.Services;
using ProjectStore.Catalogo.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
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

        [HttpGet]
        [Route("novo-produto")]
        public async Task<IActionResult> NewProduto()
        {
            return View(await PopularCategorias(new ProdutoDTO()));
        }

        [HttpPost]
        [Route("novo-produto")]
        public async Task<IActionResult> NewProduto(ProdutoDTO produtoDTO)
        {
            if (!ModelState.IsValid)
                return View(await PopularCategorias(produtoDTO));

            await _produtoAppService.AddProduto(produtoDTO);

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("editar-produto")]
        public async Task<IActionResult> UpdateProduto(Guid id)
        {
            return View(await PopularCategorias(await _produtoAppService.GetById(id)));
        }

        [HttpPost]
        [Route("editar-produto")]
        public async Task<IActionResult> UpdateProduto(Guid id, ProdutoDTO produtoDTO)
        {
            var produto = await _produtoAppService.GetById(id);
            produtoDTO.QuantidadeEstoque = produto.QuantidadeEstoque;

            if (!ModelState.IsValid)
                return View(await PopularCategorias(produtoDTO));

            await _produtoAppService.UpdateProduto(produtoDTO);

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("produtos-atualizar-estoque")]
        public async Task<IActionResult> UpdateEstoque(Guid id)
        {
            return View("Estoque", await _produtoAppService.GetById(id));
        }

        [HttpPost]
        [Route("produtos-atualizar-estoque")]
        public async Task<IActionResult> UpdateEstoque(Guid id, int quantidade)
        {
            if (quantidade > 0)
                await _produtoAppService.ReporEstoque(id, quantidade);
            else
                await _produtoAppService.DebitarEstoque(id, quantidade);

            return View("Index", await _produtoAppService.GetAll());
        }

        private async Task<ProdutoDTO> PopularCategorias(ProdutoDTO produtoDTO)
        {
            produtoDTO.Categoria = await _produtoAppService.GetCategorias();
            return produtoDTO;
        }
    }
}