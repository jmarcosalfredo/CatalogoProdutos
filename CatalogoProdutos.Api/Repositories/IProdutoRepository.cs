using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogoProdutos.Api.Models;
using CatalogoProdutos.Api.Pagination;

namespace CatalogoProdutos.Api.Repositories
{
    public interface IProdutoRepository
    {
        Task<IEnumerable<Produto>> GetAsync();
        Task<Produto> GetByIdAsync(int id);
        Task<Produto> CreateAsync(Produto produto);
        Produto Update(Produto produto);
        Produto Delete(int id);
        //Task<IEnumerable<Produto>> GetPagedAsync(ProdutosParameters produtosParams);
        Task<PagedList<Produto>> GetPagedAsync(ProdutosParameters produtosParams);
        Task<PagedList<Produto>> GetFiteredByPrecoAsync(ProdutosFiltroPreco produtosFiltroParams);
    }
}
