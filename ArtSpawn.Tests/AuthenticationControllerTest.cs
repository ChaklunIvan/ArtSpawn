using ArtSpawn.Controllers;
using ArtSpawn.Infrastructure.Interfaces;
using ArtSpawn.Models.Requests;
using ArtSpawn.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace ArtSpawn.Tests
{
    public class AuthenticationControllerTest
    {
        private readonly AuthenticationController _controllerUnderTest;
        private readonly Mock<IAuthenticationService> _mockAuthService = new();
        private readonly Mock<IUserService> _mockUserService = new();

        public AuthenticationControllerTest()
        {
            _controllerUnderTest = new AuthenticationController(_mockUserService.Object, _mockAuthService.Object);
        }

        [Fact]
        public async void RegisterUser_ShouldReturn_NewUserResponse()
        {
            //Arrange
            var userRequest = new UserRequest();
            var userResponse = new UserResponse() { Id = new Guid("11F9D973-C960-4F20-BAE8-5F54A8BBE4B5") };
            _mockUserService.Setup(x => x.CreateAsync(userRequest)).ReturnsAsync(userResponse);

            //Act
            var actual = await _controllerUnderTest.RegisterUser(userRequest);

            //Assert
            var result = Assert.IsType<OkObjectResult>(actual.Result);
            Assert.Equal(200, result.StatusCode);
            var value = Assert.IsType<UserResponse>(result.Value);
            Assert.Equal(userResponse.Id, value.Id);
        }

        [Fact]
        public async void Authenticate_ShouldReturn_()
        {
            //Arrange
            var authenticationRequest = new AuthenticationRequest() { UserName = "Test", Password = "Test123"};
            _mockAuthService.Setup(x => x.ValidateUserAsync(authenticationRequest)).ReturnsAsync(true);
            _mockAuthService.Setup(x => x.CreateTokenAsync()).ReturnsAsync("test_token");

            //Act
            var actual = await _controllerUnderTest.Authenticate(authenticationRequest);

            //
            var result = Assert.IsType<OkObjectResult>(actual);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal("{ Token = test_token }", result.Value.ToString());
        }
    }
}
