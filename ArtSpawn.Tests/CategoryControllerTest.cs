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
    public class CategoryControllerTest
    {
        private readonly CategoryController _controllerUnderTest;
        private readonly Mock<ICategoryService> _mockService = new();

        public CategoryControllerTest()
        {
            _controllerUnderTest = new CategoryController(_mockService.Object);
        }

        [Fact]
        public async void GetAllCategories_ShouldReturn_AllCategories()
        {
            //Arrange
            var categoriesResponse = new PagedList<CategoryResponse>();
            categoriesResponse.Items = new List<CategoryResponse>()
            {
                new CategoryResponse { Id = new Guid("61EDE4A9-748A-4E15-8043-7682F4C5147C") },
                new CategoryResponse { Id = new Guid("73384A2D-B59D-4DC5-9163-3FBF2AF529C4") },
                new CategoryResponse { Id = new Guid("294381F8-349E-486A-8EF3-7E901843D20A") },
            };

            _mockService.Setup(x => x.FindAllAsync(TestData.PagingRequest, TestData.CancellationToken)).ReturnsAsync(categoriesResponse);

            //Act
            var actual = await _controllerUnderTest.GetAllCategories(TestData.PagingRequest, TestData.CancellationToken);

            //Assert
            var result = Assert.IsType<ActionResultWithHeaders>(actual.Result);
            Assert.Equal(categoriesResponse.Items, result.Items);
        }

        [Fact]
        public async void GetCategory_ShouldReturn_CategoryResponse_WithSameId()
        {
            //Arrange
            var categoryResponse = new CategoryResponse() { Id = new Guid("61EDE4A9-748A-4E15-8043-7682F4C5147C") };
            _mockService.Setup(x => x.FindAsync(TestData.ModelId, TestData.CancellationToken)).ReturnsAsync(categoryResponse);

            //Act
            var actual = await _controllerUnderTest.GetCategory(TestData.ModelId, TestData.CancellationToken);

            //Assert
            var result = Assert.IsType<OkObjectResult>(actual.Result);
            Assert.Equal(200, result.StatusCode);
            var value = Assert.IsType<CategoryResponse>(result.Value);
            Assert.Equal(categoryResponse.Id, value.Id);
        }

        [Fact]
        public async void CreateCategory_ShouldReturn_CreatedCategoryResponse()
        {
            //Arrange
            var categoryRequest = new CategoryRequest();
            var categoryResponse = new CategoryResponse() { Id = new Guid("871F0E7F-6978-41E2-94E7-A8EA88805FDF") };
            _mockService.Setup(x => x.CreateAsync(categoryRequest, TestData.CancellationToken)).ReturnsAsync(categoryResponse);

            //Act
            var actual = await _controllerUnderTest.CreateCategory(categoryRequest, TestData.CancellationToken);

            //Assert
            var result = Assert.IsType<OkObjectResult>(actual.Result);
            Assert.Equal(200, result.StatusCode);
            var value = Assert.IsType<CategoryResponse>(result.Value);
            Assert.Equal(categoryResponse.Id, value.Id);
        }

        [Fact]
        public async void UpdateCategory_ShouldReturn_UpdatedCategoryResponse()
        {
            //Arrange
            var categoryUpdate = new CategoryUpdate() { Type = "Test" };
            var categoryResponse = new CategoryResponse() { Category = "Test" };
            _mockService.Setup(x => x.UpdateAsync(categoryUpdate, TestData.CancellationToken)).ReturnsAsync(categoryResponse);

            //Act
            var actual = await _controllerUnderTest.UpdateCategory(categoryUpdate, TestData.CancellationToken);

            //Assert
            var result = Assert.IsType<OkObjectResult>(actual.Result);
            Assert.Equal(200, result.StatusCode);
            var value = Assert.IsType<CategoryResponse>(result.Value);
            Assert.Equal(categoryResponse.Category, value.Category);
        }

        [Fact]
        public async void DeleteCategory_Should_DeleteCategory()
        {
            //Arrange
            var category = new Category() { Id = TestData.ModelId };
            _mockService.Setup(x => x.DeleteAsync(TestData.ModelId, TestData.CancellationToken));

            //Act
            await _controllerUnderTest.DeleteCategory(category.Id, TestData.CancellationToken);

            //Assert
            _mockService.Verify(v => v.DeleteAsync(TestData.ModelId, TestData.CancellationToken));
        }
    }
}
