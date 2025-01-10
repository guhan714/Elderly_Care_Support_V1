using Dapper;
using ElderlyCareSupport.Application.DTOs;
using ElderlyCareSupport.Application.Enums;
using ElderlyCareSupport.Application.IRepository;
using ElderlyCareSupport.Application.IService;
using ElderlyCareSupport.Domain.Models;
using ElderlyCareSupport.SQL;
using InterpolatedSql.Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ElderlyCareSupport.Infrastructure.Repository
{
    public class ElderlyUserRepository<TReturnObject,TParameter> : IUserRepository<TReturnObject,TParameter> where TReturnObject: ElderCareAccount where TParameter : ElderUserDto
    {
        private readonly IDbConnectionFactory _dbConnection;
        private readonly ILogger<ElderlyUserRepository<TReturnObject,TParameter>> _logger;

        public ElderlyUserRepository(
            ILogger<ElderlyUserRepository<TReturnObject,TParameter>> logger, IDbConnectionFactory dbConnection)
        {
            _logger = logger;
            _dbConnection = dbConnection;
        }

        public async Task<TReturnObject?> GetUserDetailsAsync(string emailId)
        {
            try
            {
                using var connection = _dbConnection.GetConnection();
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<ElderCareAccount>(UserQueries.GetUserDetailsByEmailId, new { emailId, UsersType.ElderlyUser});
                _logger.LogInformation(
                    $"The process has been started to fetch the ElderlyUserDetails... At {nameof(ElderlyUserRepository<ElderCareAccount,ElderUserDto>)}\tMethod: {nameof(GetUserDetailsAsync)}");
                return result as TReturnObject;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error Occurred During {Process} and Exception: {Message}",
                    nameof(GetUserDetailsAsync), ex.Message);
                return null;
            }
        }

        public async Task<bool> UpdateUserDetailsAsync(string emailId, TParameter elderCareAccount)
        {
            try
            {
                using var connection = _dbConnection.GetConnection();
                connection.Open();
                var successfulUpdate = await connection.ExecuteScalarAsync<int>(UserQueries.UpdateUserDetailsByEmailId, new {elderCareAccount});
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