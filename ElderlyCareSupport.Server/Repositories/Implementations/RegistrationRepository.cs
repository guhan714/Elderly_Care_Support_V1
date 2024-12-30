using AutoMapper;
using Dapper;
using ElderlyCareSupport.Server.Contexts;
using ElderlyCareSupport.Server.Models;
using ElderlyCareSupport.Server.Repositories.Interfaces;
using ElderlyCareSupport.Server.ViewModels;
using System.Data;

namespace ElderlyCareSupport.Server.Repositories.Implementations
{
    public class RegistrationRepository : IRegistrationRepository
    {
        private readonly ElderlyCareSupportContext _elderlyCareSupportContext;
        private readonly IDbConnection _dbConnection;
        private readonly ILogger<RegistrationRepository> _logger;
        private readonly IMapper _mapper;

        public RegistrationRepository(ElderlyCareSupportContext elderlyCareSupportContext,
            ILogger<RegistrationRepository> logger, IMapper mapper, IDbConnection dbConnection)
        {
            _elderlyCareSupportContext = elderlyCareSupportContext;
            _logger = logger;
            _mapper = mapper;
            _dbConnection = dbConnection;
        }

        public async Task<bool> RegisterUser(RegistrationViewModel registrationViewModel)
        {
            try
            {
                var registerModel = _mapper.Map<ElderCareAccount>(registrationViewModel);

                _logger.LogInformation(
                    $"Registration has been started at Class: {nameof(RegistrationRepository)} --> Method: {nameof(RegisterUser)}");
                var changes = await _dbConnection.ExecuteAsync(@"
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
                        nameof(registrationViewModel.Email));
                    return false;
                }

                _logger.LogInformation("Registration successful for user: {Email}",
                    nameof(registrationViewModel.Email));
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
                var isExistingUser = await _dbConnection.QuerySingleOrDefaultAsync<ElderCareAccount>("""
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