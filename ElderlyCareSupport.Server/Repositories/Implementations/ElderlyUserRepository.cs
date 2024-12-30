using AutoMapper;
using Dapper;
using ElderlyCareSupport.Server.Contexts;
using ElderlyCareSupport.Server.Controllers;
using ElderlyCareSupport.Server.DTOs;
using ElderlyCareSupport.Server.Helpers;
using ElderlyCareSupport.Server.Models;
using ElderlyCareSupport.Server.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ElderlyCareSupport.Server.Repositories.Implementations
{
    public class ElderlyUserRepository<T> : IUserRepository<T> where T : ElderUserDto, new()
    {
        private readonly ElderlyCareSupportContext _careSupportContext;
        private readonly IDbConnection _dbConnection;
        private readonly ILogger<ElderlyUserRepository<T>> _logger;
        private readonly IMapper _mapper;

        public ElderlyUserRepository(ElderlyCareSupportContext careSupportContext,
            ILogger<ElderlyUserRepository<T>> logger, IMapper mapper, IDbConnection dbConnection)
        {
            _careSupportContext = careSupportContext;
            _logger = logger;
            _mapper = mapper;
            _dbConnection = dbConnection;
        }

        public async Task<T?> GetUserDetailsAsync(string emailId)
        {
            try
            {
                var result = await
                    _dbConnection.QuerySingleOrDefaultAsync<ElderCareAccount>("""
                                                                              SELECT TOP 1 * FROM ElderCareAccount WHERE Email = @emailId
                                                                              """, new { emailId });
                _logger.LogInformation(
                    $"The process has been started to fetch the ElderlyUserDetails... At {nameof(ElderlyUserController)}\tMethod: {nameof(GetUserDetailsAsync)}");
                return DomainToDtoMapper.ToElderUserDto(result!) as T;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Occurred During {nameof(GetUserDetailsAsync)} and Exception: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> UpdateUserDetailsAsync(string emailId, T elderCareAccount)
        {
            try
            {
                var successful = await
                    _dbConnection.ExecuteAsync("""
                                                UPDATE ElderCareAccount
                                                SET FirstName = @FirstName 
                                                AND LastName = @LastName 
                                                AND Gender = @Gender
                                                AND Address = @Address
                                                AND PhoneNumber = @PhoneNumber
                                                AND City = @City
                                                AND Country = @Country
                                                AND Region = @Region
                                                AND PostalCode = @PostalCode
                                                WHERE Email = @Email
                                               """, elderCareAccount);
                return true;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError("Error occurred during {MethodName}. Exception: {ExceptionMessage}",
                    nameof(GetUserDetailsAsync), ex.Message);
                return false;
            }
        }

        [ValidateAntiForgeryToken]
        public Task<bool> DeleteUserDetailsAsync(string email)
        {
            try
            {
                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                return Task.FromResult(ex.InnerException == null);
            }
        }
    }
}