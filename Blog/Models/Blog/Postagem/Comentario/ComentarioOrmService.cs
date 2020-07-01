using System;
using System.Collections.Generic;
using System.Linq;

namespace PWABlog.Models.Blog.Postagem.Comentario
{
    public class ComentarioOrmService
    {
        private readonly DatabaseContext _databaseContext;

        public ComentarioOrmService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public ComentarioEntity ObterComentarioPorId(int id)
        {
            var comentario = _databaseContext.Comentarios.Find(id);

            return comentario;
        }

        public ComentarioEntity CriarComentario(int idPostagem, string texto, string autor, int idComentarioPai)
        {
            // Verificar existência da Postagem do Comentário
            var postagem = _databaseContext.Postagens.Find(idPostagem);
            if (postagem == null) {
                throw new Exception("A Postagem informada para o Comentário não foi encontrada!");
            }

            // Criar novo Comentário
            var novoComentario = new ComentarioEntity()
            {
                Postagem = postagem,
                Texto = texto,
                Autor = autor,
                DataCriacao = new DateTime()
            };

            // Obter e atribuir Comentário pai (caso tenha sido informado)
            if (idComentarioPai != 0) {
                var comentarioPai = _databaseContext.Comentarios.Find(idPostagem);
                if (comentarioPai == null) {
                    throw new Exception("O Comentário Pai informado para o Comentário não foi encontrado!");
                } else {
                    novoComentario.ComentarioPai = comentarioPai;
                }
            }

            // Inserir Comentário
            _databaseContext.Comentarios.Add(novoComentario);
            _databaseContext.SaveChanges();

            return novoComentario;
        }

        public ComentarioEntity EditarComentario(int id, string texto)
        {
            // Obter Comentário a Editar
            var comentario = _databaseContext.Comentarios.Find(id);
            if (comentario == null) {
                throw new Exception("Comentário não encontrado!");
            }

            // Atualizar dados do Comentário
            comentario.Texto = texto;
            _databaseContext.SaveChanges();

            return comentario;
        }

        public bool RemoverComentario(int id)
        {
            // Obter Comentário a Remover
            var comentario = _databaseContext.Comentarios.Find(id);
            if (comentario == null) {
                throw new Exception("Comentário não encontrado!");
            }

            // Remover Comentário
            _databaseContext.Comentarios.Remove(comentario);
            _databaseContext.SaveChanges();

            return true;
        }
    }
}