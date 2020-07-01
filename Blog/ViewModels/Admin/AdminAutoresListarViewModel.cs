using System.Collections.Generic;

namespace PWABlog.ViewModels.Admin
{
    public class AdminAutoresListarViewModel : ViewModelAreaAdministrativa
    {
        public string Erro { get; set; }
        public ICollection<AdminAutoresAutor> Autores { get; set; }
        public AdminAutoresListarViewModel()
        {
            TituloPagina = "Autores - Administrador";
            Autores = new List<AdminAutoresAutor>();
        }
    }
    public class AdminAutoresAutor
        {
            public int Id { get; set; }
            public string Nome { get; set; }
        }
}