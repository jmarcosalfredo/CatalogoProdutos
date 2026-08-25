using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CatalogoProdutos.Api.Models;
using Npgsql.Internal;

namespace CatalogoProdutos.Api.DTOs.Mappings
{
    public static class ProdutoDTOMappingExtensions
    {
        public static ProdutoDTO? ToProdutoDTO(this Produto produto)
        {
            if (produto is null)
            {
                return null;
            }

            var response = new ProdutoDTO
            {
                ProdutoId = produto.ProdutoId,
                Nome = produto.Nome,
                Descricao = produto.Descricao,
                ImagemUrl = produto.ImagemUrl,
                Estoque = produto.Estoque
            };

            return response;
        }

        public static Produto? ToProduto(this ProdutoDTO produtoDto)
        {
            if (produtoDto is null)
            {
                return null;
            }

            var response = new Produto
            {
                ProdutoId = produtoDto.ProdutoId,
                Nome = produtoDto.Nome,
                Descricao = produtoDto.Descricao,
                ImagemUrl = produtoDto.ImagemUrl,
                Estoque = produtoDto.Estoque,
            };

            return response;
        }

        public static IEnumerable<ProdutoDTO> ToProdutoDTOList(this IEnumerable<Produto> produtos)
        {
            if (produtos is null || !produtos.Any())
            {
                return new List<ProdutoDTO>();
            }

            var response = produtos.Select(produto => new ProdutoDTO
            {
                ProdutoId = produto.ProdutoId,
                Nome = produto.Nome,
                Descricao = produto.Descricao,
                ImagemUrl = produto.ImagemUrl,
                Estoque = produto.Estoque
            }).ToList();

            return response;
        }

        public static ProdutoDTOUpdateRequest? ProdutoToUpdateRequestDTO(this Produto produto)
        {
            if (produto is null)
            {
                return null;
            }

            var response = new ProdutoDTOUpdateRequest
            {
                Estoque = produto.Estoque,
                DataCadastro = produto.DataCadastro
            };

            return response;
        }

        public static Produto? UpdateRequestDTOToProduto(this ProdutoDTOUpdateRequest produtoDtoUpdateRequest, Produto produto)
        {
            if (produtoDtoUpdateRequest is null || produto is null)
            {
                return null;
            }

            produto.Estoque = produtoDtoUpdateRequest.Estoque;
            produto.DataCadastro = produtoDtoUpdateRequest.DataCadastro.ToUniversalTime();

            return produto;
        }

        public static ProdutoDTOUpdateResponse? ToUpdateResponseDTO(this Produto produto)
        {
            if (produto is null)
            {
                return null;
            }

            var response = new ProdutoDTOUpdateResponse
            {
                ProdutoId = produto.ProdutoId,
                Nome = produto.Nome,
                Descricao = produto.Descricao,
                Preco = produto.Preco,
                ImagemUrl = produto.ImagemUrl,
                Estoque = produto.Estoque,
                DataCadastro = produto.DataCadastro,
                CategoriaId = produto.CategoriaId,
            };

            return response;
        }
    }
}
