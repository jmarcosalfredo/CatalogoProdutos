using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogoProdutos.Api.Models;
using CatalogoProdutos.Api.Pagination;

namespace CatalogoProdutos.Api.Repositories
{
    public interface ICategoriaRepository
    {
        Task<IEnumerable<Categoria>> GetAsync();
        Task<Categoria> GetByIdAsync(int id);
        Task<Categoria> CreateAsync(Categoria categoria);
        Task<Categoria> UpdateAsync(Categoria categoria);
        Task<Categoria> DeleteAsync(int id);
        PagedList<Categoria> GetPaged(CategoriasParameters categoriasParams);
    }
}
