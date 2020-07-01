namespace PWABlog.RequestModels.AdminEtiquetas
{
    public class AdminEtiquetasEditarRequestModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int IdCategoria { get; set; }
    }
}