using AutoMapper;
using Dapper;
using ElderlyCareSupport.Server.DTOs;
using ElderlyCareSupport.Server.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Data;
using ElderlyCareSupport.Server.Contexts;
using ElderlyCareSupport.Server.Models;
using ElderlyCareSupport.Server.Services.Interfaces;

namespace ElderlyCareSupport.Server.Repositories.Implementations
{
    public class VolunteerUserRepository<T> : IUserRepository<T> where T : VolunteerUserDto, new()
    {
        private readonly ILogger<T> _logger;
        private readonly IMapper _mapper;
        private readonly IDbConnectionFactory _dbConnection;

        public VolunteerUserRepository(ILogger<T> logger, IMapper mapper, IDbConnectionFactory dbConnection)
        {
            _logger = logger;
            _mapper = mapper;
            _dbConnection = dbConnection;
        }

        public Task<bool> DeleteUserDetailsAsync(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<T?> GetUserDetailsAsync(string emailId)
        {
            try
            {
                using var connection = _dbConnection.GetConnection();
                var userDetails =
                    await connection.QueryFirstOrDefaultAsync("SELECT * FROM ElderCareAccount WHERE Email = @emailId",
                        new { emailId });
                return _mapper.Map<T>(userDetails);
            }
            catch (Exception)
            {
                return Array.Empty<string>() as T;
            }
        }

        public async Task<bool> UpdateUserDetailsAsync(string emailId, T volunteerUserDto)
        {
            try
            {
                using var connection = _dbConnection.GetConnection();
                var changesAsync = await
                    connection.ExecuteAsync("""
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
                                            """, volunteerUserDto);
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