using System.Data;
using ElderlyCareSupport.Server.Services.Interfaces;
using Microsoft.Data.SqlClient;

namespace ElderlyCareSupport.Server.Services.Implementations;

public class DbConnectionFactory: IDbConnectionFactory
{
    private readonly string _connectionString;

    public DbConnectionFactory(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    public IDbConnection GetConnection()
    {
        return new SqlConnection(_connectionString);
    }
}