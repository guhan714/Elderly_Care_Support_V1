using ElderlyCareSupport.Application.DTOs;

namespace ElderlyCareSupport.Application.Contracts.Response;

public class EmptyModelProvider
{
    public ElderUserDto EmptyElderUser { get; }= new();
    public VolunteerUserDto EmptyVolunteerUser { get; }= new();
}