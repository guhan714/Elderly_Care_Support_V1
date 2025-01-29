namespace ElderlyCareSupport.Application.Contracts.Requests;

public class TaskQueryParameters
{
    public string SearchTerm { get; set; } = string.Empty;
    public string SortBy { get; set; } = "UserType";
    public bool Ascending { get; set; } = true;
    public int pageNumber { get; set; } = 1;
    public int pageSize { get; set; } = 10;
}