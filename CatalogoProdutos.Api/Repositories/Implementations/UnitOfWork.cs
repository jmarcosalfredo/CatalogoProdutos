using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CatalogoProdutos.Api.Context;

namespace CatalogoProdutos.Api.Repositories.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private IProdutoRepository? _produtoRepo;

        private ICategoriaRepository? _categoriaRepo;
        public AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IProdutoRepository ProdutoRepository
        {
            get
            {
                if (_produtoRepo == null)
                {
                    _produtoRepo = new ProdutoRepository(_context);
                }
                return _produtoRepo;
            }
        }

        public ICategoriaRepository CategoriaRepository
        {
            get
            {
                return _categoriaRepo = _categoriaRepo ?? new CategoriaRepository(_context);
            }
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task Dispose()
        {
            await _context.DisposeAsync();
        }
    }
}
