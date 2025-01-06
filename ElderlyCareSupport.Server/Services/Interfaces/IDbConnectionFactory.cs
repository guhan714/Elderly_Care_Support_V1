using System.Data;

namespace ElderlyCareSupport.Server.Services.Interfaces;

public interface IDbConnectionFactory
{
    IDbConnection GetConnection();
}