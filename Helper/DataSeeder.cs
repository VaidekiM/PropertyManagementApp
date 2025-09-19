using PropertyManagementApp.Data; 
using PropertyManagementApp.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;

public static class DataSeeder
{
    // Used this just to populate an admin user by default
    public static void Seed(AppDbContext context)
    {
        var adminEmail = "admin@test.com";

        // Check if same user exists
        if (!context.User.Any(u => u.Email == adminEmail))
        {
            var adminUser = new User
            {
                FirstName = "Admin",
                LastName = "User",
                Email = adminEmail,
                MobileNumber = "0000000000",
                Role = "Admin"
            };

            // Using the same hasher as in registration
            var hasher = new PasswordHasher<User>();
            adminUser.PasswordHash = hasher.HashPassword(adminUser, "Admin@123");

            context.User.Add(adminUser);
            context.SaveChanges();
        }
    }
}
