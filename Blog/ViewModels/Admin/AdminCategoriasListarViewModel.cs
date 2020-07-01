using System.Collections.Generic;

namespace PWABlog.ViewModels.Admin
{
    public class AdminCategoriasListarViewModel : ViewModelAreaAdministrativa
    {
        public string Erro { get; set; }          
        public ICollection<AdminCategoriasCategoria> Categorias { get; set; }


        public AdminCategoriasListarViewModel()
        {
            TituloPagina = "Categorias - Administrador";
            Categorias = new List<AdminCategoriasCategoria>();
        }
    }

    public class AdminCategoriasCategoria
    {
        public int Id { get; set; }
        public string Nome { get; set; }
    }

}