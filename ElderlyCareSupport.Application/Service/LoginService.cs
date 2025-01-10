﻿using ElderlyCareSupport.Application.Contracts.Login;
using ElderlyCareSupport.Application.IRepository;
using ElderlyCareSupport.Application.IService;
using ElderlyCareSupport.Domain.Models;
using Microsoft.Extensions.Logging;

namespace ElderlyCareSupport.Application.Service
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _loginRepository;
        private readonly ILogger<LoginService> _logger;
        private readonly ITokenService _tokenService;

        public LoginService(ILoginRepository loginRepository, ILogger<LoginService> logger, ITokenService tokenService)
        {
            _loginRepository = loginRepository;
            _logger = logger;
            _tokenService = tokenService;
        }

        public async Task<Tuple<LoginResponse?, bool>> AuthenticateLogin(LoginRequest loginRequest)
        {
            try
            {
                _logger.LogInformation($"Started Login Authentication from {nameof(LoginService)}\nAt Method: {nameof(AuthenticateLogin)}");
                var isUserAuthenticated = await _loginRepository.AuthenticateLogin(loginRequest);
                if (!isUserAuthenticated)
                {
                    return Tuple.Create(new LoginResponse(AccessToken :string.Empty, RefreshToken: string.Empty,ExpiresIn: 0), isUserAuthenticated)!;
                }

                var token = await _tokenService.GenerateToken();

                return (string.IsNullOrEmpty(token?.AccessToken) ? Tuple.Create(new LoginResponse( AccessToken : string.Empty, RefreshToken : string.Empty,ExpiresIn : 0), isUserAuthenticated) : Tuple.Create(token, isUserAuthenticated))!;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception Occurred in the Login Authentication from {nameof(LoginService)}\nAt Method: {nameof(AuthenticateLogin)}\nException Message: {ex.Message}");
                return Tuple.Create(new LoginResponse(AccessToken :string.Empty, RefreshToken: string.Empty,ExpiresIn: 0), false)!;
            }
        }
    }
}
