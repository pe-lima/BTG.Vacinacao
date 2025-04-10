using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTG.Vacinacao.Core.Entities
{
    public class User : BaseEntity
    {
        public string Username { get; private set; }
        public string PasswordHash { get; private set; }

        public User(string username, string passwordHash)
        {
            Username = username;
            PasswordHash = passwordHash;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
