using System;
using System.ComponentModel.DataAnnotations;

namespace PWABlog.Models.Blog.Postagem.Comentario
{
    public class ComentarioEntity
    {
        [Key]
        public int Id { get; set; }
        
        public PostagemEntity Postagem { get; set; }
        
        [Required]
        public string Texto { get; set; }

        [MaxLength(128)]
        [Required]
        public string Autor { get; set; }

        [Required]
        public DateTime DataCriacao { get; set; }
        
        public ComentarioEntity ComentarioPai { get; set; }
    }
}
