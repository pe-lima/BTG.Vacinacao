using BTG.Vacinacao.Application.Commands.AuthCommand;
using BTG.Vacinacao.Application.DTOs.Auth;
using BTG.Vacinacao.Core.Interfaces.Repositories;
using BTG.Vacinacao.Core.Interfaces.Services;
using BTG.Vacinacao.CrossCutting.Exceptions;
using FluentValidation;
using MediatR;
using System.Net;

namespace BTG.Vacinacao.Application.Handlers.AuthHandler
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponseDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtService _jwtService;

        public LoginCommandHandler(IUnitOfWork unitOfWork, IJwtService jwtService)
        {
            _unitOfWork = unitOfWork;
            _jwtService = jwtService;
        }

        public async Task<LoginResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.User.GetByUsernameAsync(request.Username);

            if (user is null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                throw new GlobalException("Invalid credentials", HttpStatusCode.Unauthorized);

            var (token, expiration) = _jwtService.GenerateToken(user);

            return new LoginResponseDto
            {
                Token = token,
                Expiration = expiration
            };
        }
    }
}
