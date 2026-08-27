using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoProdutos.Api.DTOs
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "O nome de usuário é obrigatório!")]
        public string? UserName { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Informar o email é obrigatório!")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória!")]
        public string? Password { get; set; }
    }
}
