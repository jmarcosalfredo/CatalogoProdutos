using System.Collections.Generic;
using System.Threading.Tasks;
using CatalogoProdutos.Api.Controllers;
using CatalogoProdutos.Api.DTOs;
using CatalogoProdutos.Api.Models;
using CatalogoProdutos.Api.Pagination;
using CatalogoProdutos.Api.Repositories;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CatalogoProdutos.Tests.UnitTests
{
    public class ProdutosControllerTests
    {
        private readonly Mock<IUnitOfWork> _uofMock;
        private readonly Mock<IProdutoRepository> _produtoRepoMock;
        private readonly ProdutosController _controller;

        public ProdutosControllerTests()
        {
            _uofMock = new Mock<IUnitOfWork>();
            _produtoRepoMock = new Mock<IProdutoRepository>();
            _uofMock.Setup(u => u.ProdutoRepository).Returns(_produtoRepoMock.Object);

            _controller = new ProdutosController(_uofMock.Object);
        }

        [Fact]
        public async Task GetById_ShouldReturnOk_WhenExists()
        {
            //Arrange
            var produtoMock = new Produto { ProdutoId = 1, Nome = "Produto Teste", Descricao = "Desc", ImagemUrl = "img.png", Estoque = 10 };
            _produtoRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(produtoMock);

            //Act
            var product = await _controller.GetById(1);

            //Assert
            var result = product.Result.Should().BeOfType<OkObjectResult>().Subject;
            var dto = result.Value.Should().BeOfType<ProdutoDTO>().Subject;
            dto.ProdutoId.Should().Be(1);
        }

        [Fact]
        public async Task GetById_ShouldReturnNotFound_WhenNotExists()
        {
            //Arrange
            _produtoRepoMock.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Produto)null!);

            //Act
            var result = await _controller.GetById(99);

            //Assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task GetById_ShouldReturnStatus500_WhenRepoException()
        {
            //Arrange
            _produtoRepoMock.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ThrowsAsync(new System.Exception());

            //Act
            var product = await _controller.GetById(1);

            //Assert
            var result = product.Result.Should().BeOfType<ObjectResult>().Subject;
            result.StatusCode.Should().Be(500);
        }

        [Fact]
        public async Task Get_ShouldReturnOkWithList_WhenProdutosExists()
        {
            //Arrange
            var productsMock = new List<Produto>
            {
                new() { ProdutoId = 1, Nome = "P1", Descricao = "D1", ImagemUrl = "i1.png", Estoque = 5 },
                new() { ProdutoId = 2, Nome = "P2", Descricao = "D2", ImagemUrl = "i2.png", Estoque = 3 },
            };
            _produtoRepoMock.Setup(r => r.GetAsync()).ReturnsAsync(productsMock);

            //Act
            var products = await _controller.Get();

            //Assert
            var result = products.Result.Should().BeOfType<OkObjectResult>().Subject;
            var dtos = result.Value.Should().BeAssignableTo<IEnumerable<ProdutoDTO>>().Subject;
            dtos.Should().HaveCount(2);
        }

        [Fact]
        public async Task Post_ShouldReturnBadRequest_WhenDtoIsNull()
        {
            //Arrange&Act
            var result = await _controller.Post(null!);

            //Assert
            result.Result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public async Task Post_ShouldReturnCreatedAtRoute_WhenDtoIsValid()
        {
            //Arrange
            var dto = new ProdutoDTO { ProdutoId = 1, Nome = "Novo", Descricao = "Desc", ImagemUrl = "img.png", Estoque = 1 };
            _produtoRepoMock.Setup(r => r.CreateAsync(It.IsAny<Produto>())).ReturnsAsync((Produto p) => p);

            //Assert
            var product = await _controller.Post(dto);

            //Assert
            var result = product.Result.Should().BeOfType<CreatedAtRouteResult>().Subject;
            result.RouteName.Should().Be("ObterProduto");
            _produtoRepoMock.Verify(r => r.CreateAsync(It.IsAny<Produto>()), Times.Once);
            _uofMock.Verify(u => u.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task Put_ShouldReturnNotFound_WhenIdIsNotEqualsDtoId()
        {
            //Arrange
            var dto = new ProdutoDTO { ProdutoId = 2 };

            //Act
            var result = await _controller.Put(1, dto);

            //Assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task Put_ShouldReturnrOk_WhenUpdateIsValid()
        {
            //Arrange
            var dto = new ProdutoDTO { ProdutoId = 1, Nome = "Atualizado", Descricao = "Desc", ImagemUrl = "img.png", Estoque = 2 };

            //Act
            var product = await _controller.Put(1, dto);

            //Assert
            product.Result.Should().BeOfType<OkObjectResult>();
            _produtoRepoMock.Verify(r => r.Update(It.IsAny<Produto>()), Times.Once);
            _uofMock.Verify(u => u.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task Delete_ShouldReturnNotFound_WhenProdutoNotExists()
        {
            //Arrange
            _produtoRepoMock.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Produto)null!);

            //Act
            var product = await _controller.Delete(1);

            //Assert
            product.Result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task Delete_ShouldReturnOk_WhenProdutoExists()
        {
            //Arrange
            var productMock = new Produto { ProdutoId = 1, Nome = "P1", Descricao = "D1", ImagemUrl = "i.png", Estoque = 1 };
            _produtoRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(productMock);
            _produtoRepoMock.Setup(r => r.Delete(1)).Returns(productMock);

            //Act
            var result = await _controller.Delete(1);

            //Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            _uofMock.Verify(u => u.CommitAsync(), Times.Once);
        }
    }
}
