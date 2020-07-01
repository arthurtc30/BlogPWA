namespace PWABlog.RequestModels.ControleDeAcesso
{
    public class ControleDeAcessoLoginRequestModel
    {
        public string Usuario { get; set; }
        public string Senha { get; set; }
        public string Destino { get; set; }
    }
}