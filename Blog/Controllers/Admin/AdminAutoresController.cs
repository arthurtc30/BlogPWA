using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using PWABlog.Models.Blog.Autor;
using PWABlog.RequestModels.AdminAutores;
using PWABlog.ViewModels.Admin;

namespace PWABlog.Controllers.Admin
{
    [Authorize]
    public class AdminAutoresController : Controller
    {
        private readonly AutorOrmService _autoresOrmService;

        public AdminAutoresController(
            AutorOrmService autoresOrmService
        )
        {
            _autoresOrmService = autoresOrmService;
        }

        [HttpGet]
        [Route("admin/autores")]
        [Route("admin/autores/listar")]
        public IActionResult Listar()
        {
            AdminAutoresListarViewModel model = new AdminAutoresListarViewModel();

            foreach (var autor in _autoresOrmService.ObterAutores())
            {
                model.Autores.Add(new AdminAutoresAutor
                {
                    Id = autor.Id,
                    Nome = autor.Nome,

                });
            }


            return View(model);
        }
        
        [HttpGet]
        [Route("admin/autores/{id}")]
        public IActionResult Detalhar(int id)
        {
            return View();
        }

        [HttpGet]
        [Route("admin/autores/criar")]
        public IActionResult Criar()
        {
            AdminAutoresCriarViewModel model = new AdminAutoresCriarViewModel();
            model.Erro = (string)TempData["erro-msg"];

            return View(model);
        }

        [HttpPost]
        [Route("admin/autores/criar")]
        public RedirectToActionResult Criar(AdminAutoresCriarRequestModel request)
        {
            var nome = request.Nome;

            try {
                _autoresOrmService.CriarAutor(nome);
            } catch (Exception exception) {
                TempData["erro-msg"] = exception.Message;
                return RedirectToAction("Criar");
            }

            return RedirectToAction("Listar");
        }

        [HttpGet]
        [Route("admin/autores/editar/{id}")]
        public IActionResult Editar(int id)
        {

            AdminAutoresEditarViewModel model = new AdminAutoresEditarViewModel();
            var autorAeditar = _autoresOrmService.ObterAutorPorId(id);
            if (autorAeditar == null)
            {
                return RedirectToAction("Listar");
            }

            model.Erro = (string) TempData["erro-msg"];
            model.Id = autorAeditar.Id;
            model.Nome = autorAeditar.Nome;

            return View(model);
        }

        [HttpPost]
        [Route("admin/autores/editar/{id}")]
        public RedirectToActionResult Editar(AdminAutoresEditarRequestModel request)
        {
            var id = request.Id;
            var nome = request.Nome;

            try {
                _autoresOrmService.EditarAutor(id, nome);
            } catch (Exception exception) {
                TempData["erro-msg"] = exception.Message;
                return RedirectToAction("Editar", new {id = id});
            }

            return RedirectToAction("Listar");
        }

        [HttpGet]
        [Route("admin/autores/remover/{id}")]
        public IActionResult Remover(int id)
        {
            AdminAutoresRemoverViewModel model = new AdminAutoresRemoverViewModel();
            var autorARemover = _autoresOrmService.ObterAutorPorId(id);
            if (autorARemover == null)
            {
                return RedirectToAction("Listar");
            }
            model.Erro = (string)TempData["erro-msg"];

            model.Id = autorARemover.Id;
            model.Nome = autorARemover.Nome;

            return View(model);
        }

        [HttpPost]
        [Route("admin/autores/remover/{id}")]
        public RedirectToActionResult Remover(AdminAutoresRemoverRequestModel request)
        {
            var id = request.Id;

            try {
                _autoresOrmService.RemoverAutor(id);
            } catch (Exception exception) {
                TempData["erro-msg"] = exception.Message;
                return RedirectToAction("Remover", new {id = id});
            }

            return RedirectToAction("Listar");
        }
    }
}