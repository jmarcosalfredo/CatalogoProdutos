using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogoProdutos.Api.Context;
using CatalogoProdutos.Api.DTOs;
using CatalogoProdutos.Api.DTOs.Mappings;
using CatalogoProdutos.Api.Models;
using CatalogoProdutos.Api.Pagination;
using CatalogoProdutos.Api.Repositories;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatalogoProdutos.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly IUnitOfWork _uof;

        public ProdutosController(IUnitOfWork uof)
        {
            _uof = uof;
        }

        [HttpPatch("{id:int}/UpdatePartial")]
        public async Task<ActionResult<ProdutoDTOUpdateResponse>> Patch(int id, JsonPatchDocument<ProdutoDTOUpdateRequest> patchProdutoDto)
        {
            if (patchProdutoDto is null || id <= 0)
            {
                return BadRequest();
            }

            var produto = await _uof.ProdutoRepository.GetByIdAsync(id);

            if (produto is null)
            {
                return NotFound();
            }

            var produtoUpdateRequest = produto.ProdutoToUpdateRequestDTO()!;

            patchProdutoDto.ApplyTo(produtoUpdateRequest, ModelState);

            if (!ModelState.IsValid || !TryValidateModel(produtoUpdateRequest))
            {
                return BadRequest(ModelState);
            }

            produtoUpdateRequest.UpdateRequestDTOToProduto(produto);

            await _uof.ProdutoRepository.UpdateAsync(produto);
            await _uof.CommitAsync();

            var response = produto.ToUpdateResponseDTO();

            return Ok(response);
        }

        [HttpGet("pagination")]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> Get([FromQuery] ProdutosParameters produtosParams)
        {
            var produtos = await _uof.ProdutoRepository.GetPagedAsync(produtosParams);

            var produtosDto = produtos.ToProdutoDTOList();

            return Ok(produtosDto);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> Get()
        {
            try
            {
                var produtos = await _uof.ProdutoRepository.GetAsync();

                if (produtos is null)
                {
                    return NotFound();
                }

                var produtosDto = produtos.ToProdutoDTOList();

                return Ok(produtosDto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro interno no servidor.");
            }
        }

        [HttpGet("{id:int}", Name = "ObterProduto")]
        public async Task<ActionResult<ProdutoDTO>> GetById(int id)
        {
            try
            {
                var produto = await _uof.ProdutoRepository.GetByIdAsync(id);

                if (produto is null)
                {
                    return NotFound();
                }

                var produtoDto = produto.ToProdutoDTO();

                return Ok(produtoDto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro interno no servidor.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<ProdutoDTO>> Post(ProdutoDTO produtoDto)
        {
            try
            {
                if (produtoDto is null)
                {
                    return BadRequest();
                }

                var produto = produtoDto.ToProduto();

                if (produto is null)
                {
                    return BadRequest();
                }

                await _uof.ProdutoRepository.CreateAsync(produto);
                await _uof.CommitAsync();

                var novoProduto = produto.ToProdutoDTO();

                return new CreatedAtRouteResult("ObterProduto", new { id = novoProduto?.ProdutoId }, novoProduto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro interno no servidor.");
            }

        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ProdutoDTO>> Put(int id, ProdutoDTO produtoDto)
        {
            try
            {
                if (id != produtoDto.ProdutoId)
                {
                    return NotFound();
                }

                var produto = produtoDto.ToProduto();

                if (produto is null)
                {
                    return BadRequest();
                }

                await _uof.ProdutoRepository.UpdateAsync(produto);
                await _uof.CommitAsync();

                var produtoAtualizado = produto.ToProdutoDTO();

                return Ok(produtoAtualizado);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro interno no servidor.");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ProdutoDTO>> Delete(int id)
        {
            try
            {
                var produto = await _uof.ProdutoRepository.GetByIdAsync(id);

                if (produto is null)
                {
                    return NotFound();
                }

                await _uof.ProdutoRepository.DeleteAsync(id);
                await _uof.CommitAsync();

                var produtoDto = produto.ToProdutoDTO();

                return Ok(produtoDto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro interno no servidor.");
            }
        }
    }
}
