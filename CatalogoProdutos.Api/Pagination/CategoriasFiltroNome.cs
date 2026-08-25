using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoProdutos.Api.Pagination
{
    public class CategoriasFiltroNome : PaginationParameters
    {
        public string? Nome { get; set; }
    }
}
