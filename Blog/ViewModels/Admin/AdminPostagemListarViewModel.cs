using System.Collections.Generic;

namespace PWABlog.ViewModels.Admin
{
    public class AdminPostagemListarViewModel : ViewModelAreaAdministrativa
    {
        public ICollection<PostagemAdminPostagem> Postagens { get; set; }
        public AdminPostagemListarViewModel()
        {
            TituloPagina = "Postagens - Administrador";
            Postagens = new List<PostagemAdminPostagem>();
        }
    }

    public class PostagemAdminPostagem
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Categoria { get; set; }
        public string Autor { get; set; }
    }
}
