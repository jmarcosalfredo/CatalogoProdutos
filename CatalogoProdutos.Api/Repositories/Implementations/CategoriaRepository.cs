using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogoProdutos.Api.Context;
using CatalogoProdutos.Api.Models;
using CatalogoProdutos.Api.Pagination;
using Microsoft.EntityFrameworkCore;

namespace CatalogoProdutos.Api.Repositories.Implementations
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly AppDbContext _context;

        public CategoriaRepository(AppDbContext context)
        {
            _context = context;
        }

        public PagedList<Categoria> GetPaged(CategoriasParameters categoriasParams)
        {
            var categorias = _context.Categorias.AsNoTracking().ToList().OrderBy(c => c.CategoriaId).AsQueryable();
            var categoriasPaginadas = PagedList<Categoria>.ToPagedList(categorias, categoriasParams.PageNumber, categoriasParams.PageSize);

            return categoriasPaginadas;
        }

        public async Task<IEnumerable<Categoria>> GetAsync()
        {
            return await _context.Categorias.AsNoTracking().ToListAsync();
        }

        public async Task<Categoria> GetByIdAsync(int id)
        {
            var categoria = await _context.Categorias.AsNoTracking().FirstOrDefaultAsync(c => c.CategoriaId == id);

            if (categoria is null)
            {
                throw new ArgumentNullException(nameof(categoria));
            }

            return categoria;
        }

        public async Task<Categoria> CreateAsync(Categoria categoria)
        {
            if (categoria is null)
            {
                throw new ArgumentNullException(nameof(categoria));
            }

            await _context.Categorias.AddAsync(categoria);
            //await _context.SaveChangesAsync();

            return categoria;
        }

        public async Task<Categoria> UpdateAsync(Categoria categoria)
        {
            if (categoria is null)
            {
                throw new ArgumentNullException(nameof(categoria));
            }

            _context.Entry(categoria).State = EntityState.Modified;
            //await _context.SaveChangesAsync();

            return categoria;
        }

        public async Task<Categoria> DeleteAsync(int id)
        {
            var categoria = _context.Categorias.Find(id);

            if (categoria is null)
            {
                throw new ArgumentNullException(nameof(categoria));
            }

            _context.Remove(categoria);
            //await _context.SaveChangesAsync();

            return categoria;
        }
    }
}
