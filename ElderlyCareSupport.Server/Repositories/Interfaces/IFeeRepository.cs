﻿using ElderlyCareSupport.Server.DTOs;
using ElderlyCareSupport.Server.Models;

namespace ElderlyCareSupport.Server.Repositories.Interfaces
{
    public interface IFeeRepository
    {
        Task<List<FeeConfigurationDto>> GetAllFeeDetailsAsync();
    }
}
