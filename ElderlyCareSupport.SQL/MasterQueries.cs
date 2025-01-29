namespace ElderlyCareSupport.SQL;

public static class MasterQueries
{
    public static readonly string GetTaskCategories = "SELECT TaskCategoryId, CategoryName FROM dbo.TaskCategory;";
}