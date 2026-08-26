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
        Categoria Update(Categoria categoria);
        Categoria Delete(int id);
        Task<PagedList<Categoria>> GetPagedAsync(CategoriasParameters categoriasParams);
        Task<PagedList<Categoria>> GetPagedFilteredByNameAsync(CategoriasFiltroNome categoriasFiltroNomeParams);
    }
}
