namespace ElderlyCareSupport.SQL;

public static class TaskQueries
{
    public static readonly string CreateTaskQuery =
        @"INSERT INTO [dbo].[Task] (TaskName, TaskDescription, StartDate, EndDate, TaskStatusId,  ElderlyPersonId, CreatedDate, UpdatedDate) VALUES 
                                                                                                                                      (
                                                                                                                                       @TaskName,
                                                                                                                                       @TaskDescription,
                                                                                                                                       @StartDate,
                                                                                                                                       @EndDate,
                                                                                                                                       @TaskStatusId,
                                                                                                                                       @ElderlyPersonId,
                                                                                                                                       @CreatedDate,
                                                                                                                                       @UpdatedDate
                                                                                                                                      );";
    
    
    public static readonly string GetTaskById = 
        @"SELECT TaskName, TaskDescription, StartDate, EndDate, TaskStatusId, ElderlyPersonId, CreatedDate, UpdatedDate FROM [dbo].[Task] 
                                                                                                              
                                                                                                              WHERE (TaskId = @TaskId AND (@Search IS NULL OR TaskStatusId LIKE '%' + @Search + '%'))
                                                                                                              ORDER BY {0} {1}
                                                                                                              OFFSET @offSet ROWS FETCH NEXT @pageSize ROWS ONLY;";
    
    public static readonly string GetGenderById = 
        @"SELECT Gender FROM [dbo].[GenderPreference] WHERE GenderId = @GenderId";
    
    public static readonly string GetUnAssignedTask = 
        @"SELECT TaskId, TaskName,TaskDescription, StartDate, EndDate, TaskStatusId, ElderlyPersonId, CreatedDate, UpdatedDate, TaskCategoryId, PreferredGender, IsAssigned FROM [dbo].[Task] WHERE IsAssigned = @IsAssigned";
    
    
    public static readonly string GetFreeVolunteers = 
        @"SELECT * FROM VolunteerAccount WHERE IsAvailable = @IsAvailable AND Gender = @Gender AND City = @City AND PostalCode = @PostalCode AND SkillCategoryId = @SkillCategoryId;";
    
    
    public static readonly string AssignTaskToVolunteers =
        @"INSERT INTO Task_Assignment (TaskId, UserId, AssignedDate, AssignedVolunteerId) VALUES (@TaskId, @UserId, @AssignedDate, @AssignedVolunteerId)";
    
    
    public static readonly string UpdateTaskStatusAfterAssiging = 
        @"UPDATE [dbo].[Task] SET IsAssigned = @IsAssigned WHERE TaskId = @TaskId";
}