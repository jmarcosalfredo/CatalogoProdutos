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
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutosRepository _repository;

        public ProdutosController(IProdutosRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> Get()
        {
            try
            {
                var response = await _repository.GetAsync();

                if (response is null)
                {
                    return NotFound();
                }

                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro interno no servidor.");
            }
        }

        [HttpGet("{id:int}", Name = "ObterProduto")]
        public async Task<ActionResult<Produto>> GetById(int id)
        {
            try
            {
                var response = await _repository.GetByIdAsync(id);

                if (response is null)
                {
                    return NotFound();
                }

                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro interno no servidor.");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post(Produto novoProduto)
        {
            try
            {
                if (novoProduto is null)
                {
                    return BadRequest();
                }

                await _repository.CreateAsync(novoProduto);

                return new CreatedAtRouteResult("ObterProduto", new { id = novoProduto.ProdutoId }, novoProduto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro interno no servidor.");
            }

        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Produto>> Put(int id, Produto produto)
        {
            try
            {
                if (id != produto.ProdutoId)
                {
                    return BadRequest();
                }

                var existe = await _repository.GetByIdAsync(id);
                if (existe is null)
                {
                    return NotFound();
                }

                await _repository.UpdateAsync(produto);

                return Ok(produto);
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
                var produto = await _repository.GetByIdAsync(id);

                if (produto is null)
                {
                    return NotFound();
                }

                await _repository.DeleteAsync(id);

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro interno no servidor.");
            }
        }
    }
}
