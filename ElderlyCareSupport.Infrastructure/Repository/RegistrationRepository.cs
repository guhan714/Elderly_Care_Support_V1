using Dapper;
using ElderlyCareSupport.Application.Contracts;
using ElderlyCareSupport.Application.Helpers;
using ElderlyCareSupport.Application.IRepository;
using ElderlyCareSupport.Application.IService;
using ElderlyCareSupport.Application.Mapping;
using ElderlyCareSupport.Domain.Models;
using ElderlyCareSupport.SQL;
using Microsoft.Extensions.Logging;

namespace ElderlyCareSupport.Infrastructure.Repository
{
    public class RegistrationRepository : IRegistrationRepository
    {
        private readonly IDbConnectionFactory _dbConnection;
        private readonly ILogger<RegistrationRepository> _logger;

        public RegistrationRepository(
            ILogger<RegistrationRepository> logger, IDbConnectionFactory dbConnection)
        {
            _logger = logger;
            _dbConnection = dbConnection;
        }

        public async Task<bool> RegisterUser(RegistrationRequest registrationRequest)
        {
            try
            {
                using var connection = _dbConnection.GetConnection();
                connection.Open();
                var registerModel = DtoToDomainMapper.ToElderCareAccount(registrationRequest);

                _logger.LogInformation(
                    $"Registration has been started at Class: {nameof(RegistrationRepository)} --> Method: {nameof(RegisterUser)}");
                var changes = await connection.ExecuteAsync(AuthenticationQueries.RegistrationQuery, registerModel);
                
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
                connection.Open();
                var isExistingUser = await connection.QuerySingleOrDefaultAsync<ElderCareAccount>(AuthenticationQueries.ExistingUserQuery, email);
                return isExistingUser is not null;
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception Occurred. {Message}", ex.Message);
                return false;
            }
        }
    }
}