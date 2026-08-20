using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogoProdutos.Api.Context;
using CatalogoProdutos.Api.Models;
using CatalogoProdutos.Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatalogoProdutos.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriaRepository _repository;

        public CategoriasController(ICategoriaRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categoria>>> Get()
        {
            try
            {
                var categorias = await _repository.GetAsync();
                return Ok(categorias);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro interno no servidor.");
            }
        }

        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public async Task<ActionResult<Produto>> Get(int id)
        {
            try
            {
                var categoria = await _repository.GetByIdAsync(id);

                if (categoria is null)
                {
                    return NotFound($"Categoria com o id= {id} não encontrada!");
                }

                return Ok(categoria);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro interno no servidor.");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post(Categoria categoria)
        {
            try
            {
                if (categoria is null)
                {
                    return BadRequest();
                }

                var novaCategoria = await _repository.CreateAsync(categoria);

                return Created();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro interno no servidor.");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Categoria>> Put(int id, Categoria categoria)
        {
            try
            {
                if (id != categoria.CategoriaId)
                {
                    return BadRequest();
                }

                await _repository.UpdateAsync(categoria);
                return Ok(categoria);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro interno no servidor.");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var categoria = await _repository.GetByIdAsync(id);

                if (categoria is null)
                {
                    return NotFound();
                }

                await _repository.DeleteAsync(id);
                return Ok(categoria);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro interno no servidor.");
            }
        }
    }
}
