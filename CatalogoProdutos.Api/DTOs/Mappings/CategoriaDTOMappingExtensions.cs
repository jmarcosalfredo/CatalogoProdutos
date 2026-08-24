using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogoProdutos.Api.Models;

namespace CatalogoProdutos.Api.DTOs.Mappings
{
    public static class CategoriaDTOMappingExtensions
    {
        public static CategoriaDTO? ToCategoriaDTO(this Categoria categoria)
        {
            if (categoria is null)
            {
                return null;
            }

            var response = new CategoriaDTO
            {
                CategoriaId = categoria.CategoriaId,
                Nome = categoria.Nome,
                ImagemUrl = categoria.ImagemUrl
            };

            return response;
        }

        public static Categoria? ToCategoria(this CategoriaDTO categoriaDto)
        {
            if (categoriaDto is null)
            {
                return null;
            }

            var response = new Categoria
            {
                CategoriaId = categoriaDto.CategoriaId,
                Nome = categoriaDto.Nome,
                ImagemUrl = categoriaDto.ImagemUrl
            };

            return response;
        }

        public static IEnumerable<CategoriaDTO> ToCategoriaDTOList(this IEnumerable<Categoria> categorias)
        {
            if (categorias is null || !categorias.Any())
            {
                return new List<CategoriaDTO>();
            }

            var response = categorias.Select(categoria => new CategoriaDTO
            {
                CategoriaId = categoria.CategoriaId,
                Nome = categoria.Nome,
                ImagemUrl = categoria.ImagemUrl
            }).ToList();

            return response;
        }
    }
}
