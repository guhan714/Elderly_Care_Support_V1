using System.Data;

namespace ElderlyCareSupport.Application.IService;

public interface IDbConnectionFactory
{
    IDbConnection GetConnection();
}