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
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CatalogoProdutos.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriasController : ControllerBase
    {
        private readonly IUnitOfWork _uof;

        public CategoriasController(IUnitOfWork uof)
        {
            _uof = uof;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaDTO>>> Get()
        {
            try
            {
                var categorias = await _uof.CategoriaRepository.GetAsync();

                if (categorias is null)
                {
                    return NotFound();
                }

                var categoriasDto = categorias.ToCategoriaDTOList();

                return Ok(categoriasDto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro interno no servidor.");
            }
        }

        [HttpGet("pagination")]
        public ActionResult<IEnumerable<CategoriaDTO>> Get([FromQuery] CategoriasParameters categoriaParams)
        {
            try
            {
                var categorias = _uof.CategoriaRepository.GetPaged(categoriaParams);
                return ObterCategoriasPaged(categorias);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro interno no servidor.");
            }
        }

        [HttpGet("filter/nome/pagination")]
        public ActionResult<IEnumerable<CategoriaDTO>> GetFilteredByNome([FromQuery] CategoriasFiltroNome catParams)
        {
            var categoriasFiltradas = _uof.CategoriaRepository.GetPagedFilteredByName(catParams);
            return ObterCategoriasPaged(categoriasFiltradas);
        }

        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public async Task<ActionResult<CategoriaDTO>> Get(int id)
        {
            try
            {
                var categoria = await _uof.CategoriaRepository.GetByIdAsync(id);

                if (categoria is null)
                {
                    return NotFound($"Categoria com o id= {id} não encontrada!");
                }

                var categoriaDto = categoria.ToCategoriaDTO();

                return Ok(categoriaDto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro interno no servidor.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<CategoriaDTO>> Post(CategoriaDTO categoriaDto)
        {
            try
            {
                if (categoriaDto is null)
                {
                    return BadRequest();
                }

                var categoria = categoriaDto.ToCategoria();

                if (categoria is null)
                {
                    return BadRequest();
                }

                var novaCategoria = await _uof.CategoriaRepository.CreateAsync(categoria);
                await _uof.CommitAsync();

                return Created();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro interno no servidor.");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<CategoriaDTO>> Put(int id, CategoriaDTO categoriaDto)
        {
            try
            {
                if (id != categoriaDto.CategoriaId)
                {
                    return NotFound();
                }

                var categoria = categoriaDto.ToCategoria();

                if (categoria is null)
                {
                    return BadRequest();
                }

                await _uof.CategoriaRepository.UpdateAsync(categoria);
                await _uof.CommitAsync();

                var categoriaAtualizada = categoria.ToCategoriaDTO();

                return Ok(categoriaAtualizada);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro interno no servidor.");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<CategoriaDTO>> Delete(int id)
        {
            try
            {
                var categoria = await _uof.CategoriaRepository.GetByIdAsync(id);

                if (categoria is null)
                {
                    return NotFound();
                }

                await _uof.CategoriaRepository.DeleteAsync(id);
                await _uof.CommitAsync();

                var categoriaDto = categoria.ToCategoriaDTO();

                return Ok(categoriaDto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro interno no servidor.");
            }
        }

        private ActionResult<IEnumerable<CategoriaDTO>> ObterCategoriasPaged(PagedList<Categoria> categorias)
        {
            var metadata = new
            {
                categorias.TotalCount,
                categorias.PageSize,
                categorias.CurrentPage,
                categorias.TotalPages,
                categorias.HasNext,
                categorias.HasPrevious
            };

            Response.Headers.Append("Pagination", JsonConvert.SerializeObject(metadata));

            var categoriasDto = categorias.ToCategoriaDTOList();

            return Ok(categoriasDto);
        }
    }
}
