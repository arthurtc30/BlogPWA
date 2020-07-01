using System;
using System.ComponentModel.DataAnnotations;

namespace PWABlog.Models.Blog.Postagem.Revisao
{
    public class RevisaoEntity
    {
        [Key]
        public int Id { get; set; }
        
        public PostagemEntity Postagem { get; set; }
        
        [MaxLength(128)]
        [Required]
        public string Texto { get; set; }
        
        [Required]
        public int Versao { get; set; }
        
        [Required]
        public DateTime DataCriacao { get; set; }
    }
}
