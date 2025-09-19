using Microsoft.AspNetCore.Identity;

namespace PropertyManagementApp.Models;
public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; }  // username
    public string MobileNumber { get; set; }
    public string PasswordHash { get; set; }
    public bool IsAdmin { get; set; } 
    public string Role { get; set; }  // Admin or User
}