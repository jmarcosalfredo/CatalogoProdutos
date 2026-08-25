using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoProdutos.Api.Pagination
{
    public class ProdutosFiltroPreco : PaginationParameters
    {
        public decimal? Preco { get; set; }
        public string? PrecoCriterio { get; set; } // "maior", "menor", "igual"
    }
}
