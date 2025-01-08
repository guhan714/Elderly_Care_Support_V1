using AutoMapper;
using Dapper;
using ElderlyCareSupport.Server.Models;
using ElderlyCareSupport.Server.Repositories.Interfaces;
using ElderlyCareSupport.Server.ViewModels;
using System.Data;
using ElderlyCareSupport.Server.Contexts;
using ElderlyCareSupport.Server.Services.Interfaces;

namespace ElderlyCareSupport.Server.Repositories.Implementations
{
    public class RegistrationRepository : IRegistrationRepository
    {
        private readonly IDbConnectionFactory _dbConnection;
        private readonly ILogger<RegistrationRepository> _logger;
        private readonly IMapper _mapper;

        public RegistrationRepository(
            ILogger<RegistrationRepository> logger, IMapper mapper, IDbConnectionFactory dbConnection)
        {
            _logger = logger;
            _mapper = mapper;
            _dbConnection = dbConnection;
        }

        public async Task<bool> RegisterUser(RegistrationRequest registrationRequest)
        {
            try
            {
                using var connection = _dbConnection.GetConnection();
                var registerModel = _mapper.Map<ElderCareAccount>(registrationRequest);

                _logger.LogInformation(
                    $"Registration has been started at Class: {nameof(RegistrationRepository)} --> Method: {nameof(RegisterUser)}");
                var changes = await connection.ExecuteAsync(@"
                                INSERT INTO ElderCareAccount (
                                    FirstName, LastName, Email, Password, ConfirmPassword, PhoneNumber, Gender, Address, City, Region, PostalCode, Country, UserType, IsActive
                                )
                                VALUES (
                                    @FirstName, @LastName, @Email, @Password, @ConfirmPassword, @PhoneNumber, @Gender, @Address, @City, @Region, @PostalCode, @Country, @UserType, @IsActive
                                );", registerModel);
                
                if (changes == 0)
                {
                    _logger.LogWarning(
                        "Registration failed, no changes were saved for user: {registrationViewModel.Email}",
                        nameof(registrationRequest.Email));
                    return false;
                }

                _logger.LogInformation("Registration successful for user: {Email}",
                    nameof(registrationRequest.Email));
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    "Registration has been Failed at Class: {RegistrationRepository} --> Method: {RegisterUser}\nException Occurred: {Message}",
                    nameof(RegistrationRepository), nameof(RegisterUser), ex.Message);
                return false;
            }
        }


        public async Task<bool> CheckExistingUser(string email)
        {
            try
            {
                using var connection = _dbConnection.GetConnection();
                var isExistingUser = await connection.QuerySingleOrDefaultAsync<ElderCareAccount>("""
                    SELECT COUNT(*) FROM ElderCareAccount WHERE Email = @email;
                    """, new { email });
                return isExistingUser is null;
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception Occurred. {Message}", ex.Message);
                return false;
            }
        }
    }
}