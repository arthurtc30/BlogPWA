using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWABlog.ViewModels.Admin
{
    public class AdminAutoresCriarViewModel : ViewModelAreaAdministrativa
    {
        public string Nome { get; set; }

        public string Erro { get; set; }

        public AdminAutoresCriarViewModel()
        {
            TituloPagina = "Criar novo autor";
        }
    }
}
