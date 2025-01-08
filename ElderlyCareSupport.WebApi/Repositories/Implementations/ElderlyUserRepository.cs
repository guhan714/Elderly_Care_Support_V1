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
using ElderlyCareSupport.Server.Contracts;
using ElderlyCareSupport.Server.Contracts.Common;
using ElderlyCareSupport.Server.Services.Interfaces;
using InterpolatedSql.Dapper;
using MethodTimer;

namespace ElderlyCareSupport.Server.Repositories.Implementations
{
    public class ElderlyUserRepository<TReturnObject,TParameter> : IUserRepository<TReturnObject,TParameter> where TReturnObject: ElderCareAccount where TParameter : ElderUserDto
    {
        private readonly IDbConnectionFactory _dbConnection;
        private readonly ILogger<ElderlyUserRepository<TReturnObject,TParameter>> _logger;
        private readonly IMapper _mapper;

        public ElderlyUserRepository(
            ILogger<ElderlyUserRepository<TReturnObject,TParameter>> logger, IMapper mapper, IDbConnectionFactory dbConnection)
        {
            _logger = logger;
            _mapper = mapper;
            _dbConnection = dbConnection;
        }

        [Time]
        public async Task<TReturnObject?> GetUserDetailsAsync(string emailId)
        {
            try
            {
                using var connection = _dbConnection.GetConnection();
                var query = connection.SqlBuilder($"SELECT TOP 1 * FROM ElderCareAccount WHERE Email = {emailId};");
                var result = await query.QuerySingleOrDefaultAsync<ElderCareAccount?>();
                _logger.LogInformation(
                    $"The process has been started to fetch the ElderlyUserDetails... At {nameof(ElderlyUserController)}\tMethod: {nameof(GetUserDetailsAsync)}");
                return result as TReturnObject;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error Occurred During {Process} and Exception: {Message}",
                    nameof(GetUserDetailsAsync), ex.Message);
                return null;
            }
        }

        [Time]
        public async Task<bool> UpdateUserDetailsAsync(string emailId, TParameter elderCareAccount)
        {
            try
            {
                using var connection = _dbConnection.GetConnection();
                var query = connection.SqlBuilder($"""
                                                      UPDATE ElderCareAccount
                                                      SET FirstName = {elderCareAccount.FirstName} 
                                                      ,LastName = {elderCareAccount.LastName}
                                                      ,Gender = {elderCareAccount.Gender}
                                                      ,Address = {elderCareAccount.Address}
                                                      ,PhoneNumber = {elderCareAccount.PhoneNumber}
                                                      ,City = {elderCareAccount.City}
                                                      ,Country = {elderCareAccount.Country}
                                                      ,Region = {elderCareAccount.Region}
                                                      ,PostalCode = {elderCareAccount.PostalCode}
                                                      WHERE Email = {elderCareAccount.Email}; 
                                                      """
                );
                var successfulUpdate = await query.ExecuteScalarAsync<int>();
                return successfulUpdate >= 1;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError("Error occurred during {MethodName}. Exception: {ExceptionMessage}",
                    nameof(UpdateUserDetailsAsync), ex.Message);
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