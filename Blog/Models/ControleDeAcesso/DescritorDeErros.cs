using Microsoft.AspNetCore.Identity;

namespace PWABlog.Models.ControleDeAcesso
{
    public class DescritorDeErros : IdentityErrorDescriber
    {
        public override IdentityError DuplicateUserName(string userName)
        {
            var error = base.DuplicateUserName(userName);
            error.Description = "O nome de usuário informado já existe";
            return error;
        }

        public override IdentityError DuplicateEmail(string email)
        {
            var erro = base.DuplicateEmail(email);
            erro.Description = "O email informado já existe";
            return erro;
        }

        public override IdentityError PasswordTooShort(int length)
        {
            var error = base.PasswordTooShort(length);
            error.Description = "A senha precisa de pelo menos " + length + " caracteres";
            return error;
        }

        public override IdentityError PasswordRequiresNonAlphanumeric()
        {
            var error = base.PasswordRequiresNonAlphanumeric();
            error.Description = "A senha precisa de pelo menos um caracter não alfanumérico";
            return error;
        }

        public override IdentityError PasswordRequiresLower()
        {
            var erro = base.PasswordRequiresLower();
            erro.Description = "A senha precisa de pelo menos um caracter minúsculo";
            return erro;
        }
    }
}