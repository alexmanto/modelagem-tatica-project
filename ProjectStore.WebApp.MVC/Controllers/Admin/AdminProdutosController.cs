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


        private async Task<ProdutoDTO> PopularCategorias(ProdutoDTO produtoDTO)
        {
            produtoDTO.Categoria = await _produtoAppService.GetCategorias();
            return produtoDTO;
        }
    }
}