namespace ElderlyCareSupport.Server.DTOs
{
    public class ElderUserDTO
    {
        public string FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Gender {  get; set; } = string.Empty;
        public string Address {  get; set; } = string.Empty;
        public string City {  get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public string Country {  get; set; } = string.Empty;
        public long PhoneNumber { get; set; }
        public long PostalCode { get; set; } 
        public long UserType { get; set; }
        
    }
}
