using System.Text.Json.Serialization;

namespace ElderlyCareSupport.Application.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum UsersType
    {
        Admin = 1,
        ElderlyUser = 2,
        Volunteer = 3
    }
}
