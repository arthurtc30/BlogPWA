using System;
using System.Collections.Generic;
using Microsoft.VisualBasic;

namespace PWABlog.ViewModels.Admin
{
    public class AdminPostagemCriarViewModel : ViewModelAreaAdministrativa
    {
        public ICollection<AutorAdminPostagem> Autores { get; set; }
        public ICollection<CategoriaAdminPostagem> Categorias { get; set; }
        public ICollection<EtiquetaAdminPostagem> Etiquetas { get; set; }

        public string Erro { get; set; }

        public AdminPostagemCriarViewModel()
        {
            TituloPagina = "Criar nova postagem";
            Autores = new List<AutorAdminPostagem>();
            Categorias = new List<CategoriaAdminPostagem>();
            Etiquetas = new List<EtiquetaAdminPostagem>();

        }
    }

    public class AutorAdminPostagem
    {
        public int Id { get; set; }
        public string Nome { get; set; }
    }
    public class CategoriaAdminPostagem
    {
        public int Id { get; set; }
        public string Nome { get; set; }
    }

    public class EtiquetaAdminPostagem
    {
        public int Id { get; set; }
        public string Nome { get; set; }
    }

}
