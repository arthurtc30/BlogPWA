namespace PWABlog.RequestModels.AdminPostagens
{
    public class AdminPostagensEditarRequestModel
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public int IdCategoria { get; set; }
        public string Texto { get; set; }
        public string DataExibicao { get; set; }
    }
}