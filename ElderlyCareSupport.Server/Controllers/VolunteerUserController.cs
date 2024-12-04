using ElderlyCareSupport.Server.Common;
using ElderlyCareSupport.Server.DTOs;
using ElderlyCareSupport.Server.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Runtime.CompilerServices;

namespace ElderlyCareSupport.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Volunteer")]
    public class VolunteerUserController() : ControllerBase
    {

    }
}
