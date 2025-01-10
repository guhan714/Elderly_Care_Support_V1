namespace ElderlyCareSupport.SQL;

public static class UserQueries
{
    public static string GetUserDetailsByEmailId =>
        "SELECT  (FirstName, LastName, Email, PhoneNumber, Gender, Address, City, Region, PostalCode, Country, UserType, IsActive) FROM ElderCareAccount WHERE Email = @email AND UserType = @UserType;";

    public static string UpdateUserDetailsByEmailId => " UPDATE ElderCareAccount SET FirstName = @firstName, LastName = @lastName,Gender = @gender,Address = @address,PhoneNumber = @phoneNumber,City = @city,Country = @country,Region = @region,PostalCode = @postalCode WHERE Email = @email AND UserType = @UserType; ";
    
}