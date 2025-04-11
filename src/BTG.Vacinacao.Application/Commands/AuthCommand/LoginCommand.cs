using BTG.Vacinacao.Application.DTOs.Auth;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTG.Vacinacao.Application.Commands.AuthCommand
{
    public record LoginCommand(string Username, string Password) : IRequest<LoginResponseDto>;
}
