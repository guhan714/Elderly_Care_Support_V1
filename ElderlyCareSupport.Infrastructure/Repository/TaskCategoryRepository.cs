using Dapper;
using ElderlyCareSupport.Application.IRepository;
using ElderlyCareSupport.Application.IService;
using ElderlyCareSupport.Domain.Models;
using ElderlyCareSupport.SQL;

namespace ElderlyCareSupport.Infrastructure.Repository;

public class TaskCategoryRepository : ITaskMasterRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public TaskCategoryRepository(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task<List<TaskCategory>> GetTaskCategories()
    {
        var connection = _dbConnectionFactory.GetConnection();
        var result = await connection.QueryAsync<TaskCategory>(MasterQueries.GetTaskCategories);
        return result.ToList();
    }
}