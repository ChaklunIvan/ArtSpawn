using ArtSpawn.Controllers;
using ArtSpawn.Helpers.Headers;
using ArtSpawn.Infrastructure.Interfaces;
using ArtSpawn.Models.Entities;
using ArtSpawn.Models.Requests;
using ArtSpawn.Models.Responses;
using ArtSpawn.Models.Updates;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace ArtSpawn.Tests
{
    public class ProductControllerTest
    {
        private readonly ProductController _controllerUnderTest;
        private readonly Mock<IProductService> _mockService = new();

        public ProductControllerTest()
        {
            _controllerUnderTest = new ProductController(_mockService.Object);
        }

        [Fact]
        public async void GetAllProducts_ShouldReturn_AllProducts()
        {
            //Arrange
            var productResponse = new PagedList<ProductResponse>();
            productResponse.Items = new List<ProductResponse>()
            {
                new ProductResponse { Id = new Guid("61EDE4A9-748A-4E15-8043-7682F4C5147C") },
                new ProductResponse { Id = new Guid("73384A2D-B59D-4DC5-9163-3FBF2AF529C4") },
                new ProductResponse { Id = new Guid("294381F8-349E-486A-8EF3-7E901843D20A") },
            };

            _mockService.Setup(x => x.FindAllAsync(TestData.PagingRequest)).ReturnsAsync(productResponse);

            //Act
            var actual = await _controllerUnderTest.GetAllProduct(TestData.PagingRequest);

            //Assert
            var result = Assert.IsType<ActionResultWithHeaders>(actual.Result);
            Assert.Equal(productResponse.Items, result.Items);
        }

        [Fact]
        public async void GetProduct_ShouldReturn_ProductResponse_WithSameId()
        {
            //Arrange
            var productResponse = new ProductResponse() { Id = new Guid("61EDE4A9-748A-4E15-8043-7682F4C5147C") };
            _mockService.Setup(x => x.FindAsync(TestData.ModelId, TestData.CancellationToken)).ReturnsAsync(productResponse);

            //Act
            var actual = await _controllerUnderTest.GetProduct(TestData.ModelId, TestData.CancellationToken);

            //Assert
            var result = Assert.IsType<OkObjectResult>(actual.Result);
            Assert.Equal(200, result.StatusCode);
            var value = Assert.IsType<ProductResponse>(result.Value);
            Assert.Equal(productResponse.Id, value.Id);
        }

        [Fact]
        public async void GetProductsByArtist_ShouldReturn_ProductsByArtistPaged()
        {
            //Arrange
            var productResponse = new PagedList<ProductResponse>();
            var artistId = new Guid("61EDE4A9-748A-4E15-8043-7682F4C5147C");
            productResponse.Items = new List<ProductResponse>()
            {
                new ProductResponse { Id = new Guid("61EDE4A9-748A-4E15-8043-7682F4C5147C"), ArtistId = TestData.ModelId },
                new ProductResponse { Id = new Guid("73384A2D-B59D-4DC5-9163-3FBF2AF529C4"), ArtistId = TestData.ModelId },
                new ProductResponse { Id = new Guid("294381F8-349E-486A-8EF3-7E901843D20A"), ArtistId = TestData.ModelId },
            };
            _mockService.Setup(x => x.FindByAsync(TestData.PagingRequest, s => s.ArtistId == artistId)).ReturnsAsync(productResponse);

            //Act
            var actual = await _controllerUnderTest.GetProductsByArtist(TestData.PagingRequest, artistId);

            //Assert
            var result = Assert.IsType<ActionResultWithHeaders>(actual.Result);
            Assert.Equal(productResponse.Items, result.Items);
        }

        [Fact]
        public async void GetProductsByCategory_ShouldReturn_ProductsByCategoryPaged()
        {
            //Arrange
            var productResponse = new PagedList<ProductResponse>();
            var categoryId = new Guid("61EDE4A9-748A-4E15-8043-7682F4C5147C");
            productResponse.Items = new List<ProductResponse>()
            {
                new ProductResponse { Id = new Guid("61EDE4A9-748A-4E15-8043-7682F4C5147C"), CategoryId = TestData.ModelId },
                new ProductResponse { Id = new Guid("73384A2D-B59D-4DC5-9163-3FBF2AF529C4"), CategoryId = TestData.ModelId },
                new ProductResponse { Id = new Guid("294381F8-349E-486A-8EF3-7E901843D20A"), CategoryId = TestData.ModelId },
            };
            _mockService.Setup(x => x.FindByAsync(TestData.PagingRequest, s => s.CategoryId == categoryId)).ReturnsAsync(productResponse);

            //Act
            var actual = await _controllerUnderTest.GetProductsByCategory(TestData.PagingRequest, categoryId, TestData.CancellationToken);

            //Assert
            var result = Assert.IsType<ActionResultWithHeaders>(actual.Result);
            Assert.Equal(productResponse.Items, result.Items);
        }

        [Fact]
        public async void CreateProduct_ShouldReturn_CreatedProductResponse()
        {
            //Arrange
            var productRequest = new ProductRequest();
            var productResponse = new ProductResponse() { Id = new Guid("871F0E7F-6978-41E2-94E7-A8EA88805FDF") };
            _mockService.Setup(x => x.CreateAsync(productRequest, TestData.CancellationToken)).ReturnsAsync(productResponse);

            //Act
            var actual = await _controllerUnderTest.CreateProduct(productRequest, TestData.CancellationToken);

            //Assert
            var result = Assert.IsType<OkObjectResult>(actual.Result);
            Assert.Equal(200, result.StatusCode);
            var value = Assert.IsType<ProductResponse>(result.Value);
            Assert.Equal(productResponse.Id, value.Id);
        }

        [Fact]
        public async void UpdateProduct_ShouldReturn_UpdatedProductResponse()
        {
            //Arrange
            var productUpdate = new ProductUpdate() { Title = "Test" };
            var productResponse = new ProductResponse() { Title = "Test" };
            _mockService.Setup(x => x.UpdateAsync(productUpdate, TestData.CancellationToken)).ReturnsAsync(productResponse);

            //Act
            var actual = await _controllerUnderTest.UpdateProduct(productUpdate, TestData.CancellationToken);

            //Assert
            var result = Assert.IsType<OkObjectResult>(actual.Result);
            Assert.Equal(200, result.StatusCode);
            var value = Assert.IsType<ProductResponse>(result.Value);
            Assert.Equal(productResponse.Title, value.Title);
        }

        [Fact]
        public async void DeleteProduct_Should_DeleteProduct()
        {
            //Arrange
            var product = new Product() { Id = TestData.ModelId };
            _mockService.Setup(x => x.DeleteAsync(TestData.ModelId, TestData.CancellationToken));

            //Act
            await _controllerUnderTest.DeleteProduct(product.Id, TestData.CancellationToken);

            //Assert
            _mockService.Verify(v => v.DeleteAsync(TestData.ModelId, TestData.CancellationToken));
        }
    }
}

