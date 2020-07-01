using System.ComponentModel.DataAnnotations;
using PWABlog.Models.Blog.Etiqueta;

namespace PWABlog.Models.Blog.Postagem
{
    public class PostagemEtiquetaEntity
    {
        [Key]
        public int Id { get; set; }
        
        public PostagemEntity Postagem { get; set; }
        
        public EtiquetaEntity Etiqueta { get; set; }
    }
}