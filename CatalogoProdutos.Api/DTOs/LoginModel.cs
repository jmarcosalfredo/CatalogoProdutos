using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoProdutos.Api.DTOs
{
    public class LoginModel
    {
        [Required(ErrorMessage = "O nome de usuário é obrigatório!")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória!")]
        public string? Password { get; set; }
    }
}
