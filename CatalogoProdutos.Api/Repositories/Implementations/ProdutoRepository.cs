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
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly AppDbContext _context;

        public ProdutoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Produto>> GetPagedAsync(ProdutosParameters produtosParams)
        {
            return _context.Produtos.AsNoTracking().ToList().OrderBy(p => p.Nome).Skip((produtosParams.PageNumber - 1) * produtosParams.PageSize).Take(produtosParams.PageSize).ToList();
        }

        public async Task<IEnumerable<Produto>> GetAsync()
        {
            return await _context.Produtos.AsNoTracking().ToListAsync();
        }

        public async Task<Produto> GetByIdAsync(int id)
        {
            var produto = await _context.Produtos.AsNoTracking().FirstOrDefaultAsync(p => p.ProdutoId == id);

            if (produto is null)
            {
                throw new ArgumentNullException(nameof(produto));
            }

            return produto;
        }

        public async Task<Produto> CreateAsync(Produto produto)
        {
            if (produto is null)
            {
                throw new ArgumentNullException(nameof(produto));
            }

            await _context.Produtos.AddAsync(produto);
            //await _context.SaveChangesAsync();

            return produto;
        }

        public async Task<Produto> UpdateAsync(Produto produto)
        {
            if (produto is null)
            {
                throw new ArgumentNullException(nameof(produto));
            }

            _context.Entry(produto).State = EntityState.Modified;
            //await _context.SaveChangesAsync();

            return produto;
        }

        public async Task<Produto> DeleteAsync(int id)
        {
            var produto = _context.Produtos.Find(id);

            if (produto is null)
            {
                throw new ArgumentNullException(nameof(produto));
            }

            _context.Remove(produto);
            //await _context.SaveChangesAsync();

            return produto;
        }
    }
}
