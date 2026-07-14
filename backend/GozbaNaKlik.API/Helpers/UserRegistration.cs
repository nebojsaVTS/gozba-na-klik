using GozbaNaKlik.API.Data;
using GozbaNaKlik.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GozbaNaKlik.API.Helpers
{
    // Obicna staticka pomocna metoda (ne DI servis) da se izbegne
    // kopiranje logike za kreiranje korisnika u vise kontrolera.
    public static class UserRegistration
    {
        private static readonly PasswordHasher<User> Hasher = new();

        public static async Task<(bool Success, string? Error, User? User)> TryCreateUserAsync(
            AppDbContext context, string username, string email, string password, string role)
        {
            bool exists = await context.Users.AnyAsync(u =>
                u.Username == username || u.Email == email);

            if (exists)
            {
                return (false, "Korisnik sa tim username-om ili email-om već postoji.", null);
            }

            var user = new User
            {
                Username = username,
                Email = email,
                Role = role
            };
            user.Password = Hasher.HashPassword(user, password);

            context.Users.Add(user);
            await context.SaveChangesAsync();

            return (true, null, user);
        }
    }
}
