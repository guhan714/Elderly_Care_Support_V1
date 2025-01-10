using Dapper;
using ElderlyCareSupport.Application.DTOs;
using ElderlyCareSupport.Application.IRepository;
using ElderlyCareSupport.Application.IService;
using ElderlyCareSupport.Domain.Models;
using ElderlyCareSupport.SQL;
using InterpolatedSql.Dapper;
using InterpolatedSql.SqlBuilders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ElderlyCareSupport.Infrastructure.Repository
{
    public class VolunteerUserRepository<TReturnObject, TParameter> : IUserRepository<TReturnObject,TParameter> where TReturnObject : VolunteerAccount where TParameter : VolunteerUserDto
    {
        private readonly ILogger<VolunteerUserRepository<TReturnObject,TParameter>> _logger;
        private readonly IDbConnectionFactory _dbConnection;

        public VolunteerUserRepository(ILogger<VolunteerUserRepository<TReturnObject,TParameter>> logger,  IDbConnectionFactory dbConnection)
        {
            _logger = logger;
            _dbConnection = dbConnection;
        }

        public Task<bool> DeleteUserDetailsAsync(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<TReturnObject?> GetUserDetailsAsync(string emailId)
        {
            try
            {
                using var connection = _dbConnection.GetConnection();
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<VolunteerAccount>(UserQueries.GetUserDetailsByEmailId, new { emailId });
                return result as TReturnObject;
            }
            catch (Exception)
            {
                return Array.Empty<string>() as TReturnObject;
            }
        }

        public async Task<bool> UpdateUserDetailsAsync(string emailId, TParameter volunteerUserDto)
        {
            try
            {
                using var connection = _dbConnection.GetConnection();
                connection.Open();
                var changesAsync = await
                    connection.ExecuteAsync(UserQueries.UpdateUserDetailsByEmailId, new {volunteerUserDto});
                return changesAsync > 0;
            }
            catch (DbUpdateConcurrencyException exception)
            {
                _logger.LogError("Error updating Database {Exception}", exception.Message);
                return false;
            }
        }
    }
}