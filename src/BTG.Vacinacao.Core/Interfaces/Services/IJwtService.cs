using BTG.Vacinacao.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTG.Vacinacao.Core.Interfaces.Services
{
    public interface IJwtService
    {
        (string Token, DateTime Expiration) GenerateToken(User user);
    }
}
