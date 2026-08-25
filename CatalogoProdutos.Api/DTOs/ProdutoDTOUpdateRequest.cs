using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoProdutos.Api.DTOs
{
    public class ProdutoDTOUpdateRequest : IValidatableObject
    {
        public float Estoque { get; set; }
        public DateTimeOffset DataCadastro { get; set; } = DateTimeOffset.Now;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (DataCadastro.Date < DateTime.Now.Date)
            {
                yield return new ValidationResult("Data de Cadastro Inválida!", new[] { nameof(this.DataCadastro) });
            }
        }
    }
}
