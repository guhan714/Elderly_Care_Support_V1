using AutoMapper;
using Dapper;

using ElderlyCareSupport.Server.Controllers;
using ElderlyCareSupport.Server.DTOs;
using ElderlyCareSupport.Server.Helpers;
using ElderlyCareSupport.Server.Models;
using ElderlyCareSupport.Server.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using ElderlyCareSupport.Server.Contexts;
using InterpolatedSql.Dapper;

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
                var query = _dbConnection.SqlBuilder($"SELECT TOP 1 * FROM ElderCareAccount WHERE Email = {emailId};");
                var result = await query.QuerySingleOrDefaultAsync<ElderCareAccount?>();
                _logger.LogInformation(
                    $"The process has been started to fetch the ElderlyUserDetails... At {nameof(ElderlyUserController)}\tMethod: {nameof(GetUserDetailsAsync)}");
                return result is not null
                    ? DomainToDtoMapper.ToElderUserDto(result) as T
                    : EmptyModels.EmptyElderUser as T;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error Occurred During {Process} and Exception: {Message}", nameof(GetUserDetailsAsync), ex.Message);
                return null;
            }
        }

        public async Task<bool> UpdateUserDetailsAsync(string emailId, T elderCareAccount)
        {
            try
            {
                var query = _dbConnection.SqlBuilder($"""
                                                      UPDATE ElderCareAccount
                                                      SET FirstName = {elderCareAccount.FirstName} 
                                                      AND LastName = {elderCareAccount.LastName}
                                                      AND Gender = {elderCareAccount.Gender}
                                                      AND Address = {elderCareAccount.Address}
                                                      AND PhoneNumber = {elderCareAccount.PhoneNumber}
                                                      AND City = {elderCareAccount.City}
                                                      AND Country = {elderCareAccount.Country}
                                                      AND Region = {elderCareAccount.Region}
                                                      AND PostalCode = {elderCareAccount.PostalCode}
                                                      WHERE Email = {elderCareAccount.Email}; 
                                                      """
                    );
                var successfulUpdate = await query.ExecuteScalarAsync<int>();
                return successfulUpdate >= 1;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError("Error occurred during {MethodName}. Exception: {ExceptionMessage}",
                    nameof(GetUserDetailsAsync), ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error Occurred connecting to server or processing {Exception}", ex.Message);
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