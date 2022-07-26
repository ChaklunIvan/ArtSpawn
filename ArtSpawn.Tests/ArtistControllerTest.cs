using ArtSpawn.Controllers;
using ArtSpawn.Database;
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
    public class ArtistControllerTest
    {
        private readonly ArtistController _controllerUnderTest;
        private readonly Mock<IArtistService> _mockService = new();

        public ArtistControllerTest()
        {
            _controllerUnderTest = new ArtistController(_mockService.Object);
        }

        [Fact]
        public async void GetAllArtists_ShouldReturn_AllArtists()
        {
            //Arrange
            var artistsResponse = new PagedList<ArtistResponse>();
            artistsResponse.Items = new List<ArtistResponse>()
            {
                new ArtistResponse { Id = new Guid("61EDE4A9-748A-4E15-8043-7682F4C5147C") },
                new ArtistResponse { Id = new Guid("73384A2D-B59D-4DC5-9163-3FBF2AF529C4") },
                new ArtistResponse { Id = new Guid("294381F8-349E-486A-8EF3-7E901843D20A") },
            };

            _mockService.Setup(x => x.FindAllAsync(TestData.PagingRequest, TestData.CancellationToken)).ReturnsAsync(artistsResponse);

            //Act
            var actual = await _controllerUnderTest.GetAllArtists(TestData.PagingRequest, TestData.CancellationToken);

            //Assert
            var result = Assert.IsType<ActionResultWithHeaders>(actual.Result);
            Assert.Equal(artistsResponse.Items, result.Items);
        }

        [Fact]
        public async void GetArtist_ShouldReturn_ArtistResponse_WithSameId()
        {
            //Arrange
            var artistResponse = new ArtistResponse() { Id = new Guid("61EDE4A9-748A-4E15-8043-7682F4C5147C") };
            _mockService.Setup(x => x.FindAsync(TestData.ModelId, TestData.CancellationToken)).ReturnsAsync(artistResponse);

            //Act
            var actual = await _controllerUnderTest.GetArtist(TestData.ModelId, TestData.CancellationToken);

            //Assert
            var result = Assert.IsType<OkObjectResult>(actual.Result);
            Assert.Equal(200, result.StatusCode);
            var value = Assert.IsType<ArtistResponse>(result.Value);
            Assert.Equal(artistResponse.Id, value.Id);
        }

        [Fact]
        public async void CreateArtist_ShouldReturn_CreatedArtistResponse()
        {
            //Arrange
            var artistRequest = new ArtistRequest();
            var artistResponse = new ArtistResponse() { Id = new Guid("871F0E7F-6978-41E2-94E7-A8EA88805FDF") };
            _mockService.Setup(x => x.CreateAsync(artistRequest, TestData.CancellationToken)).ReturnsAsync(artistResponse);

            //Act
            var actual = await _controllerUnderTest.CreateArtist(artistRequest, TestData.CancellationToken);

            //Assert
            var result = Assert.IsType<OkObjectResult>(actual.Result);
            Assert.Equal(200, result.StatusCode);
            var value = Assert.IsType<ArtistResponse>(result.Value);
            Assert.Equal(artistResponse.Id, value.Id);
        }

        [Fact]
        public async void UpdateArtist_ShouldReturn_UpdatedArtistResponse()
        {
            //Arrange
            var artistUpdate = new ArtistUpdate() { Name = "Test"};
            var artistResponse = new ArtistResponse() { Name = "Test" };
            _mockService.Setup(x => x.UpdateAsync(artistUpdate, TestData.CancellationToken)).ReturnsAsync(artistResponse);

            //Act
            var actual = await _controllerUnderTest.UpdateArtist(artistUpdate, TestData.CancellationToken);

            //Assert
            var result = Assert.IsType<OkObjectResult>(actual.Result);
            Assert.Equal(200, result.StatusCode);
            var value = Assert.IsType<ArtistResponse>(result.Value);
            Assert.Equal(artistResponse.Name, value.Name);
        }

        [Fact]
        public async void DeleteArtist_Should_DeleteArtist()
        {
            //Arrange
            var artist = new Artist() { Id = TestData.ModelId };
            _mockService.Setup(x => x.DeleteAsync(TestData.ModelId, TestData.CancellationToken));

            //Act
            await _controllerUnderTest.DeleteArtist(artist.Id, TestData.CancellationToken);

            //Assert
            _mockService.Verify(v => v.DeleteAsync(TestData.ModelId, TestData.CancellationToken));
        }
    }
}