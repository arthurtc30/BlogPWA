using System;
using System.Collections.Generic;
using System.Linq;

namespace PWABlog.Models.Blog.Postagem.Classificacao
{
    public class ClassificacaoOrmService
    {
        private readonly DatabaseContext _databaseContext;

        public ClassificacaoOrmService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public ClassificacaoEntity ObterClassificacaoPorId(int id)
        {
            var classificacao = _databaseContext.Classificacoes.Find(id);

            return classificacao;
        }

        public ClassificacaoEntity CriarClassificacao(int idPostagem, bool classificacaoDada)
        {
            // Verificar existência da Postagem da Classificação
            var postagem = _databaseContext.Postagens.Find(idPostagem);
            if (postagem == null) {
                throw new Exception("A Postagem informada para a Classificação não foi encontrada!");
            }

            // Criar nova Classificação
            var novaClassificacao = new ClassificacaoEntity()
            {
                Postagem = postagem,
                Classificacao = classificacaoDada
            };
            _databaseContext.Classificacoes.Add(novaClassificacao);
            _databaseContext.SaveChanges();

            return novaClassificacao;
        }

        public ClassificacaoEntity EditarEtiqueta(int id, bool classificacaoDada)
        {
            // Obter Classificação a Editar
            var classificacao = _databaseContext.Classificacoes.Find(id);
            if (classificacao == null) {
                throw new Exception("Classificação não encontrada!");
            }

            // Atualizar dados da Classificação
            classificacao.Classificacao = classificacaoDada;
            _databaseContext.SaveChanges();

            return classificacao;
        }

        public bool RemoverClassificacao(int id)
        {
            // Obter Classificação a Remover
            var classificacao = _databaseContext.Classificacoes.Find(id);
            if (classificacao == null) {
                throw new Exception("Classificação não encontrada!");
            }

            // Remover Classificação
            _databaseContext.Classificacoes.Remove(classificacao);
            _databaseContext.SaveChanges();

            return true;
        }
    }
}