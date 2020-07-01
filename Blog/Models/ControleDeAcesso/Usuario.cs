using Microsoft.AspNetCore.Identity;

namespace PWABlog.Models.ControleDeAcesso
{
    public class Usuario : IdentityUser<int>
    {
        public string Apelido { get; set; }
    }
}