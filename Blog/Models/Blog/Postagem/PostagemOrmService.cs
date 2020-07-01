using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PWABlog.Models.Blog.Postagem.Revisao;

namespace PWABlog.Models.Blog.Postagem
{
    public class PostagemOrmService
    {
        private readonly DatabaseContext _databaseContext;

        private readonly RevisaoOrmService _revisaoOrmService;

        public PostagemOrmService(
            DatabaseContext databaseContext,
            RevisaoOrmService revisaoOrmService
        )
        {
            _databaseContext = databaseContext;
            _revisaoOrmService = revisaoOrmService;
        }

        public List<PostagemEntity> ObterPostagens()
        {
            return _databaseContext.Postagens
                .Include(p => p.Categoria)
                .Include(p => p.Revisoes)
                .Include(p => p.Comentarios)
                .ToList();
        }

        public List<PostagemEntity> ObterPostagensPopulares()
        {
            return _databaseContext.Postagens
                .Include(a => a.Autor)
                .OrderByDescending(c => c.Comentarios.Count)
                .Take(4)
                .ToList();
        }
        
        public PostagemEntity ObterPostagemPorId(int idPostagem)
        {
            var postagem = _databaseContext.Postagens.Find(idPostagem);

            return postagem;
        }

        public PostagemEntity CriarPostagem(string titulo, string descricao, int idAutor, int idCategoria, string texto)
        {
            // Verificar existência do Autor da Postagem
            var autor = _databaseContext.Autores.Find(idAutor);
            if (autor == null) {
                throw new Exception("O Autor informado para a Postagem não foi encontrado!");
            }

            // Verificar existência da Categoria da Postagem
            var categoria = _databaseContext.Categorias.Find(idCategoria);
            if (categoria == null) {
                throw new Exception("A Categoria informada para a Postagem não foi encontrada!");
            }

            // Criar nova Postagem
            var novaPostagem = new PostagemEntity
            {
                Titulo = titulo,
                Descricao = descricao,
                Autor = autor,
                Categoria = categoria,
            };
            _databaseContext.Postagens.Add(novaPostagem);
            _databaseContext.SaveChanges();
            
            // Criar a Revisão para a Postagem
            _revisaoOrmService.CriarRevisao(novaPostagem.Id, texto);
            
            return novaPostagem;
        }

        public PostagemEntity EditarPostagem(int id, string titulo, string descricao, int idCategoria, string texto, DateTime dataExibicao)
        {
            // Obter Postagem a Editar
            var postagem = _databaseContext.Postagens.Find(id);
            if (postagem == null) {
                throw new Exception("Postagem não encontrada!");
            }

            // Verificar existência da Categoria da Postagem
            var categoria = _databaseContext.Categorias.Find(idCategoria);
            if (categoria == null) {
                throw new Exception("A Categoria informada para a Postagem não foi encontrada!");
            }

            // Atualizar dados da Postagem
            postagem.Titulo = titulo;
            postagem.Descricao = descricao;
            postagem.Categoria = categoria;
            
            _databaseContext.SaveChanges();
            
            // Criar nova Revisão para a Postagem
            _revisaoOrmService.CriarRevisao(postagem.Id, texto);

            return postagem;
        }

        public bool RemoverPostagem(int id)
        {
            // Obter Postagem a Remover
            var postagem = _databaseContext.Postagens.Find(id);
            if (postagem == null) {
                throw new Exception("Postagem não encontrada!");
            }

            // Remover Postagem
            _databaseContext.Postagens.Remove(postagem);
            _databaseContext.SaveChanges();

            return true;
        }
    }
}