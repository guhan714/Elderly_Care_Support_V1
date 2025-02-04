using System.Net;
using ElderlyCareSupport.Application.Common;
using ElderlyCareSupport.Application.Contracts.Response;
using ElderlyCareSupport.Application.DTOs;
using ElderlyCareSupport.Application.Enums;
using ElderlyCareSupport.Application.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using LoginRequest = ElderlyCareSupport.Application.Contracts.Requests.LoginRequest;

namespace ElderlyCareSupportTesting;

using ElderlyCareSupport.Server.Controllers;

public class MockHomeController
{
    private readonly ElderlyCareSupportHomeController _mockController;
    private readonly Mock<IFeeService> _mockFeeService;
    private readonly Mock<IApiResponseFactoryService> _mockApiResponseFactory;
    private readonly Mock<ILogger<ElderlyCareSupportHomeController>> _logger;


    public MockHomeController()
    {
        _mockFeeService = new Mock<IFeeService>();
        _logger = new Mock<ILogger<ElderlyCareSupportHomeController>>();
        _mockApiResponseFactory = new Mock<IApiResponseFactoryService>();
        
        _mockController = new ElderlyCareSupportHomeController(
           _mockFeeService.Object,
           _logger.Object,
           _mockApiResponseFactory.Object
           );
    }

    [Fact]
    private async Task GetAllFeeDetails_ShouldReturnFeeDetails_WhenFeesAreAvailable()
    {
        // Arrange
        var feeDetails = new List<FeeConfigurationDto>()
        {
            new()
            {
                FeeId = 1,
                FeeName = "Tech Fee",
                FeeAmount = 1,
                Description = "A fee charged for the use of technology-related services or resources, such as software, hardware, or IT support."
            },
            new()
            {
                FeeId = 2,
                FeeName = "Convenience Fee",
                FeeAmount = 2,
                Description = "An additional charge applied for the convenience of using certain payment methods or accessing services remotely, often for tasks like bill payments or ticket bookings."
            },
            new()
            {
                FeeId = 3,
                FeeName = "Grocery Shopping",
                FeeAmount = 100,
                Description = "The activity of purchasing food and household items from a grocery store or supermarket."
            },
            new()
            {
                FeeId = 4,
                FeeName = "Medical care",
                FeeAmount = 300,
                Description = "Health-related services provided by medical professionals to diagnose, treat, and manage illnesses, injuries, or health conditions."
            },new()
            {
                FeeId = 5,
                FeeName = "Tech Fee",
                FeeAmount = 1,
                Description = "A fee charged for the use of technology-related services or resources, such as software, hardware, or IT support."
            },
            new()
            {
                FeeId = 6,
                FeeName = "Convenience Fee",
                FeeAmount = 2,
                Description = "An additional charge applied for the convenience of using certain payment methods or accessing services remotely, often for tasks like bill payments or ticket bookings."
            },
            new()
            {
                FeeId = 7,
                FeeName = "Grocery Shopping",
                FeeAmount = 100,
                Description = "The activity of purchasing food and household items from a grocery store or supermarket."

            },
            new()
            {
                FeeId = 8,
                FeeName = "Medical care",
                FeeAmount = 300,
                Description = "Health-related services provided by medical professionals to diagnose, treat, and manage illnesses, injuries, or health conditions."

            }
            
        };
        _mockFeeService.Setup(service => service.GetAllFeeDetails()).ReturnsAsync(feeDetails);
        _mockApiResponseFactory.Setup(service => service.CreateResponse(
            It.IsAny<IEnumerable<FeeConfigurationDto>>(), 
            true, 
            Constants.StatusMessageOk, 
            HttpStatusCode.OK, 
            It.IsAny<string>(),
            null)
        ).Returns(new ApiResponseModel<IEnumerable<FeeConfigurationDto>>
        {
            Success = true,
            Data = feeDetails,
            StatusMessage = Constants.StatusMessageOk
        });
        
        
        // Act
        var actualResult = await _mockController.GetFeeDetails();
        
        // Assert
        var okResult = Assert.IsType<OkObjectResult>(actualResult);
        var response = Assert.IsType<ApiResponseModel<IEnumerable<FeeConfigurationDto>>>(okResult.Value);
        Assert.True(response.Success);
        Assert.NotNull(actualResult);
    }


    [Theory]
    [InlineData("user@sample.com", "stringst",  UsersType.ElderlyUser)]
    private async Task AuthenticateUser_ShouldReturnUnauthorized_WhenUserIsNotAuthenticated(string userEmail, string password, UsersType userType)
    {
        var loginRequest = new LoginRequest
        {
            Email = userEmail,
            Password = password,
            UserType = userType
        };
        var loginResponse = (new LoginResponse
        (
            AccessToken : "",
            ExpiresIn : 0,
            RefreshToken : ""
        ), false);

    }
}