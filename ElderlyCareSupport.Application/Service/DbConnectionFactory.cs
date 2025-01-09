using System.Data;
using ElderlyCareSupport.Application.IService;
using Microsoft.Data.SqlClient;

namespace ElderlyCareSupport.Application.Service;

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