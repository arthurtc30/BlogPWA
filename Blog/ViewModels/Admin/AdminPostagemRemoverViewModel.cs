namespace PWABlog.ViewModels.Admin
{
    public class AdminPostagemRemoverViewModel : ViewModelAreaAdministrativa
    {
        public int Id { get; set; }

        public string Titulo { get; set; }

        public string Erro { get; set; }

        public AdminPostagemRemoverViewModel()
        {
            TituloPagina = "Remover Postagem: ";
        }
    }
}