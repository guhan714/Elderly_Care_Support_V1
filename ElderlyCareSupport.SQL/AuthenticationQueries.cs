namespace ElderlyCareSupport.SQL;

public static class AuthenticationQueries
{
    public static readonly string AllFeeDetailsQuery = "SELECT FEE_ID as FeeId, FEE_NAME as FeeName, FEE_AMOUNT as FeeAmount, Description FROM dbo.FEE_CONFIGURATION;";
    public static string LoginQuery => "SELECT PASSWORD FROM DBO.ElderCareAccount WHERE Email = @Email AND UserType = @UserType;";

    public static string RegistrationQuery =>
        "INSERT INTO ElderCareAccount (FirstName, LastName, Email, Password,  PhoneNumber, Gender, Address, City, Region, PostalCode, Country, UserType, IsActive) VALUES(@FirstName, @LastName, @Email, @Password,  @PhoneNumber, @Gender, @Address, @City, @Region, @PostalCode, @Country, @UserType, @IsActive);";

    public static string ForgotPasswordQuery => "SELECT PASSWORD FROM DBO.ElderCareAccount WHERE Email = @UserName;";

    public static string ExistingUserQuery => "SELECT COUNT(*) FROM ElderCareAccount WHERE Email = @email;";
}