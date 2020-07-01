using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWABlog.ViewModels.Admin
{
    public class AdminCategoriasCriarViewModel : ViewModelAreaAdministrativa
    {
        public string Erro { get; set; }

        public string Nome { get; set; }

        public AdminCategoriasCriarViewModel()
        {
            TituloPagina = "Criar nova Categoria";
            
        }
    }
}
