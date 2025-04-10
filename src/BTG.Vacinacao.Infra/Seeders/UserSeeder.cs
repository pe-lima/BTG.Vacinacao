using BTG.Vacinacao.Core.Entities;
using BTG.Vacinacao.Infra.Context;
using Microsoft.Extensions.DependencyInjection;
using BCrypt.Net;

namespace BTG.Vacinacao.Infra.Seeders
{
    public static class UserSeeder
    {
        public static void Seed(IServiceProvider services)
        {
            var context = services.GetRequiredService<ApplicationDbContext>();

            if (!context.Users.Any())
            {
                var password = BCrypt.Net.BCrypt.HashPassword("Admin@123");
                var user = new User("admin_btg", password);
                context.Users.Add(user);
                context.SaveChanges();
            }
        }
    }

}
