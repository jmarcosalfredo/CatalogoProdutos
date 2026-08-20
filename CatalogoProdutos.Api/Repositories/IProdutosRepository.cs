using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogoProdutos.Api.Models;

namespace CatalogoProdutos.Api.Repositories
{
    public interface IProdutosRepository
    {
        Task<IEnumerable<Produto>> GetAsync();
        Task<Produto> GetByIdAsync(int id);
        Task<Produto> CreateAsync(Produto produto);
        Task<Produto> UpdateAsync(Produto produto);
        Task<Produto> DeleteAsync(int id);
    }
}
