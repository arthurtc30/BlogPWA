using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PWABlog.Models.Blog.Categoria;
using PWABlog.RequestModels.AdminCategorias;
using PWABlog.ViewModels.Admin;

namespace PWABlog.Controllers.Admin
{
    [Authorize]
    public class AdminCategoriasController : Controller
    {
        private readonly CategoriaOrmService _categoriaOrmService;

        public AdminCategoriasController(
            CategoriaOrmService categoriaOrmService
        )
        {
            _categoriaOrmService = categoriaOrmService;
        }

        [HttpGet]
        public IActionResult Listar()
        {
            AdminCategoriasListarViewModel model = new AdminCategoriasListarViewModel();
            foreach (var categoria in _categoriaOrmService.ObterCategorias())
            {
                model.Categorias.Add(new AdminCategoriasCategoria
                {
                    Id = categoria.Id,
                    Nome = categoria.Nome
                });
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Detalhar(int id)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Criar()
        {
            AdminCategoriasCriarViewModel model = new AdminCategoriasCriarViewModel();
            model.Erro = (string) TempData["erro-msg"];
            return View(model);
        }

        [HttpPost]
        public RedirectToActionResult Criar(AdminCategoriasCriarRequestModel request)
        {
            var nome = request.Nome;

            try {
                _categoriaOrmService.CriarCategoria(nome);
            } catch (Exception exception) {
                TempData["erro-msg"] = exception.Message;
                return RedirectToAction("Criar");
            }

            return RedirectToAction("Listar");
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            AdminCategoriasEditarViewModel model = new AdminCategoriasEditarViewModel();
            var categoriaAEditar  = _categoriaOrmService.ObterCategoriaPorId(id);
            if (categoriaAEditar == null)
            {
                return RedirectToAction("Listar");
            }
            model.Erro = (string)TempData["erro-msg"];
            model.Id = categoriaAEditar.Id;
            model.Nome = categoriaAEditar.Nome;
           

            return View(model);
            

        }

        [HttpPost]
        public RedirectToActionResult Editar(AdminCategoriasEditarRequestModel request)
        {
            var id = request.Id;
            var nome = request.Nome;

            try {
                _categoriaOrmService.EditarCategoria(id, nome);
            } catch (Exception exception) {
                TempData["erro-msg"] = exception.Message;
                return RedirectToAction("Editar", new {id = id});
            }

            return RedirectToAction("Listar");
        }

        [HttpGet]
        public IActionResult Remover(int id)
        {

            AdminCategoriasRemoverViewModel model = new AdminCategoriasRemoverViewModel();
            var categoriaAEditar = _categoriaOrmService.ObterCategoriaPorId(id);
            if (categoriaAEditar == null)
            {
                return RedirectToAction("Listar");
            }
            model.Erro = (string)TempData["erro-msg"];
            model.Id = categoriaAEditar.Id;
            model.Nome = categoriaAEditar.Nome;
            model.TituloPagina += model.Nome;

            return View(model);
        }

        [HttpPost]
        public RedirectToActionResult Remover(AdminCategoriasRemoverRequestModel request)
        {
            var id = request.Id;

            try {
                _categoriaOrmService.RemoverCategoria(id);
            } catch (Exception exception) {
                TempData["erro-msg"] = exception.Message;
                return RedirectToAction("Remover", new {id = id});
            }

            return RedirectToAction("Listar");
        }
    }
}