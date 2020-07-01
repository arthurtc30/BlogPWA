using System.Collections.Generic;

namespace PWABlog.ViewModels.Admin
{
    public class AdminEtiquetasCriarViewModel : ViewModelAreaAdministrativa
    {
        public string Erro { get; set; }

        public ICollection<CategoriaAdminEtiquetas> Categorias { get; set; }
        
        
        public AdminEtiquetasCriarViewModel()
        {
            TituloPagina = "Criar nova Etiqueta";
            Categorias = new List<CategoriaAdminEtiquetas>();
        }
    }

    public class CategoriaAdminEtiquetas
    {
        public int IdCategoria { get; set; }
        public string NomeCategoria { get; set; }
    }
}