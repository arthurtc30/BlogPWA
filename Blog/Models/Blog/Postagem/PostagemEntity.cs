using System;
using PWABlog.Models.Blog.Autor;
using PWABlog.Models.Blog.Categoria;
using System.Collections.Generic;
using PWABlog.Models.Blog.Postagem.Revisao;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using PWABlog.Models.Blog.Postagem.Classificacao;
using PWABlog.Models.Blog.Postagem.Comentario;

namespace PWABlog.Models.Blog.Postagem
{
    public class PostagemEntity
    {
        [Key] public int Id { get; set; }

        [MaxLength(128)] [Required] public string Titulo { get; set; }

        [MaxLength(640)] [Required] public string Descricao { get; set; }

        public AutorEntity Autor { get; set; }

        public CategoriaEntity Categoria { get; set; }

        public List<PostagemEtiquetaEntity> PostagensEtiquetas { get; set; }

        public ICollection<RevisaoEntity> Revisoes { get; set; }

        public ICollection<ComentarioEntity> Comentarios { get; set; }

        public ICollection<ClassificacaoEntity> Classificacoes { get; set; }

        
        public PostagemEntity()
        {
            PostagensEtiquetas = new List<PostagemEtiquetaEntity>();
            Revisoes = new List<RevisaoEntity>();
            Comentarios = new List<ComentarioEntity>();
            Classificacoes = new List<ClassificacaoEntity>();
        }

        public RevisaoEntity ObterUltimaRevisao()
        {
            return Revisoes.OrderByDescending(r => r.Versao).FirstOrDefault();
        }
    }
}