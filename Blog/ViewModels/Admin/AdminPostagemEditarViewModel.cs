using System.Collections.Generic;

namespace PWABlog.ViewModels.Admin
{
    public class AdminPostagemEditarViewModel : ViewModelAreaAdministrativa
    {
        public ICollection<AutorAdminPostagem> Autores { get; set; }
        public ICollection<CategoriaAdminPostagem> Categorias { get; set; }
        public ICollection<EtiquetaAdminPostagem> Etiquetas { get; set; }
        public ICollection<int> EtiquetasPostagem { get; set; }

        public int Id { get; set; }
        public int IdAutor { get; set; }
        public int IdCategoria { get; set; }
        public string Titulo { get; set; }
        public string Texto { get; set; }
        public string Erro { get; set; }

        public AdminPostagemEditarViewModel()
        {
            TituloPagina = "Editar postagem";
            Autores = new List<AutorAdminPostagem>();
            Categorias = new List<CategoriaAdminPostagem>();
            Etiquetas = new List<EtiquetaAdminPostagem>();
            EtiquetasPostagem = new List<int>();
        }
    }
}
