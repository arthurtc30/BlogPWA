using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PWABlog.Models.Blog.Autor;
using PWABlog.Models.Blog.Categoria;
using PWABlog.Models.Blog.Etiqueta;
using PWABlog.Models.Blog.Postagem;
using PWABlog.RequestModels.AdminPostagens;
using PWABlog.ViewModels.Admin;

namespace PWABlog.Controllers.Admin
{
    [Authorize]
    public class AdminPostagensController : Controller
    {
        private readonly PostagemOrmService _postagemOrmService;
        private readonly CategoriaOrmService _categoriaOrmService;
        private readonly AutorOrmService _autorOrmService;
        private readonly EtiquetaOrmService _etiquetaOrmService;

        public AdminPostagensController(
            PostagemOrmService postagemOrmService,
            CategoriaOrmService CategoriaOrmService,
            AutorOrmService AutorOrmService,
            EtiquetaOrmService EtiquetaOrmService)
        {
            _postagemOrmService = postagemOrmService;
            _categoriaOrmService = CategoriaOrmService;
            _autorOrmService = AutorOrmService;
            _etiquetaOrmService = EtiquetaOrmService;
        }

        [HttpGet]
        public IActionResult Listar()
        {
            AdminPostagemListarViewModel model = new AdminPostagemListarViewModel();
            foreach (var postagem in _postagemOrmService.ObterPostagens())
            {
                model.Postagens.Add(
                    new PostagemAdminPostagem()
                    {
                        Id = postagem.Id,
                        Titulo = postagem.Titulo,
                        Categoria = postagem.Categoria.Nome,
                        Autor = postagem.Autor.Nome
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
            AdminPostagemCriarViewModel model = new AdminPostagemCriarViewModel();
            foreach (var autor in _autorOrmService.ObterAutores())
            {
                model.Autores.Add(new AutorAdminPostagem
                {
                    Id = autor.Id,
                    Nome = autor.Nome
                });
            }

            foreach (var categoria in _categoriaOrmService.ObterCategorias())
            {
                model.Categorias.Add(new CategoriaAdminPostagem
                {
                    Id = categoria.Id,
                    Nome = categoria.Nome
                });
            }

            foreach (var etiqueta in _etiquetaOrmService.ObterEtiquetas())
            {
                model.Etiquetas.Add(new EtiquetaAdminPostagem
                {
                    Id = etiqueta.Id,
                    Nome = etiqueta.Nome
                });
            }

            model.Erro = (string)TempData["erro-msg"];
            return View(model);
        }

        [HttpPost]
        public RedirectToActionResult Criar(AdminPostagensCriarRequestModel request)
        {
            var titulo = request.Texto;
            var descricao = request.Descricao;
            var idAutor = request.IdAutor;
            var idCategoria = request.IdCategoria;
            var texto = request.Texto;
            
            
            try {
                _postagemOrmService.CriarPostagem(titulo, descricao, idAutor, idCategoria, texto);
            } catch (Exception exception) {
                TempData["erro-msg"] = exception.Message;
                return RedirectToAction("Criar");
            }

            return RedirectToAction("Listar");
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            AdminPostagemEditarViewModel model = new AdminPostagemEditarViewModel
            {
                Erro = (string) TempData["erro-msg"]
            };

            foreach (var autor in _autorOrmService.ObterAutores())
            {
                model.Autores.Add(new AutorAdminPostagem
                {
                    Id = autor.Id,
                    Nome = autor.Nome
                });
            }

            foreach (var categoria in _categoriaOrmService.ObterCategorias())
            {
                model.Categorias.Add(new CategoriaAdminPostagem
                {
                    Id = categoria.Id,
                    Nome = categoria.Nome
                });
            }

            foreach (var etiqueta in _etiquetaOrmService.ObterEtiquetas())
            {
                model.Etiquetas.Add(new EtiquetaAdminPostagem
                {
                    Id = etiqueta.Id,
                    Nome = etiqueta.Nome
                });
            }

            var postagem = _postagemOrmService.ObterPostagemPorId(id);
            model.Id = postagem.Id;
            model.IdAutor = postagem.Autor.Id;
            model.IdCategoria = postagem.Categoria.Id;
            model.Titulo = postagem.Titulo;
            model.Texto = postagem.Revisoes.OrderByDescending
                (rev => rev.Versao).Last().Texto;

            foreach (var etiqueta in postagem.PostagensEtiquetas)
            {
                model.EtiquetasPostagem.Add(etiqueta.Etiqueta.Id);
            }

            return View(model);
        }

        [HttpPost]
        public RedirectToActionResult Editar(AdminPostagensEditarRequestModel request)
        {
            var id = request.Id;
            var titulo = request.Texto;
            var descricao = request.Descricao;
            var idCategoria = Convert.ToInt32(request.IdCategoria);
            var texto = request.Texto;
            var dataExibicao = DateTime.Parse(request.DataExibicao);

            try {
                _postagemOrmService.EditarPostagem(id, titulo, descricao, idCategoria, texto, dataExibicao);
            } catch (Exception exception) {
                TempData["erro-msg"] = exception.Message;
                return RedirectToAction("Editar", new {id = id});
            }

            return RedirectToAction("Listar");
        }

        [HttpGet]
        public IActionResult Remover(int id)
        {
            var postagem = _postagemOrmService.ObterPostagemPorId(id);
            var model = new AdminPostagemRemoverViewModel
            {
                Id = postagem.Id,
                Titulo = postagem.Titulo,
                Erro = (string)TempData["erro-msg"]
            };

            return View(model);
        }

        [HttpPost]
        public RedirectToActionResult Remover(AdminPostagensRemoverRequestModel request)
        {
            var id = request.Id;

            try {
                _postagemOrmService.RemoverPostagem(id);
            } catch (Exception exception) {
                TempData["erro-msg"] = exception.Message;
                return RedirectToAction("Remover", new {id = id});
            }

            return RedirectToAction("Listar");
        }
    }
}