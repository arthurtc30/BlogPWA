using System;
using System.Collections.Generic;
using System.Linq;

namespace PWABlog.Models.Blog.Postagem.Revisao
{
    public class RevisaoOrmService
    {
        private readonly DatabaseContext _databaseContext;

        public RevisaoOrmService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public RevisaoEntity ObterRevisaoPorId(int id)
        {
            var revisao = _databaseContext.Revisoes.Find(id);

            return revisao;
        }

        public RevisaoEntity CriarRevisao(int idPostagem, string texto)
        {
            // Verificar existência da Postagem da Revisão
            var postagem = _databaseContext.Postagens.Find(idPostagem);
            if (postagem == null) {
                throw new Exception("A Postagem informada para a Revisão não foi encontrada!");
            }

            // Criar nova Revisão
            var novaRevisao = new RevisaoEntity()
            {
                Postagem = postagem,
                Texto = texto,
                Versao = postagem.ObterUltimaRevisao().Versao + 1,
                DataCriacao = new DateTime()
            };
            _databaseContext.Revisoes.Add(novaRevisao);
            _databaseContext.SaveChanges();

            return novaRevisao;
        }

        public RevisaoEntity EditarRevisao(int id, string texto)
        {
            // Obter Revisão a Editar
            var revisao = _databaseContext.Revisoes.Find(id);
            if (revisao == null) {
                throw new Exception("Revisão não encontrada!");
            }

            // Atualizar dados da Revisão
            revisao.Texto = texto;
            _databaseContext.SaveChanges();

            return revisao;
        }

        public bool RemoverRevisao(int id)
        {
            // Obter Revisão a Remover
            var revisao = _databaseContext.Revisoes.Find(id);
            if (revisao == null) {
                throw new Exception("Revisão não encontrada!");
            }

            // Remover Revisão
            _databaseContext.Revisoes.Remove(revisao);
            _databaseContext.SaveChanges();

            return true;
        }
    }
}