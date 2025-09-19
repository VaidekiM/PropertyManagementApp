using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using PropertyManagementApp.Data;
using Microsoft.AspNetCore.Identity;
using PropertyManagementApp.Models;
using PropertyManagementApp.DTO;

public class AccountController : Controller
{
    private readonly AppDbContext _context;
    private readonly ILogger<AccountController> _logger;

    public AccountController(AppDbContext context, ILogger<AccountController> logger)
    {
        _context = context;
        _logger = logger;
    }

    // Registeration logic starts
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Register(RegisterDTO model)
    {
        if (!ModelState.IsValid) {
            _logger.LogWarning("Registration form validation failed for email: {Email}", model.Email);
            return View(model);
        }

        // To avoid duplicae user accounts: Check if email or mobile number already exists
        if (_context.User.Any(u => u.Email == model.Email) || _context.User.Any(u => u.MobileNumber == model.MobileNumber))
        {
            _logger.LogWarning("Duplicate registration attempt for email: {Email} or mobile: {Mobile}", model.Email, model.MobileNumber);
            TempData["WarningMessage"] = "Email or mobile number already registered. Please login or use a different one.";
            return View(model);
        }

        // For now I'm allowing only 1 admin user in the system hence added this logic
        Console.WriteLine($"IsAdmin value received: {model.IsAdmin}");
        if (model.IsAdmin)
        {
            bool adminExists = _context.User.Any(u => u.IsAdmin);
            if (adminExists)
            {
                _logger.LogWarning("Admin registration attempt blocked for email: {Email}. Admin already exists.", model.Email);
                ModelState.AddModelError("", "Admin user already exists. Please contact the administrator for admin access.");
                model.IsAdmin = false;
                return View(model);
            }
        }

        var user = new User
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            MobileNumber = model.MobileNumber,
            IsAdmin = model.IsAdmin,
            Role = model.IsAdmin ? "Admin" : "User"
        };

        // I used this Identity's hash method to avoid 
        // storing password explicitly in the DB
        var hasher = new PasswordHasher<User>();
        user.PasswordHash = hasher.HashPassword(user, model.Password);

        _context.User.Add(user);
        _context.SaveChanges();

        _logger.LogInformation("New user registered successfully. Email: {Email}, Role: {Role}", user.Email, user.Role);
        TempData["SuccessMessage"] = "Account created successfully! Please login.";
        // Redirect to login after successful registration
        return RedirectToAction("Login");
    }

    // Login logic starts here
    public IActionResult Login()
    {
        _logger.LogInformation("Login page accessed at {Time}", DateTime.UtcNow);
        return View();
    }

    [HttpPost]
    public IActionResult Login(string email, string password)
    {
        _logger.LogInformation("Login attempt for email {Email} at {Time}", email, DateTime.UtcNow);
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            ViewBag.Error = "Please enter both email and password";
            return View();
        }

        var user = _context.User.FirstOrDefault(u => u.Email == email);
        if (user != null)
        {
            //Used to verify hashed password with user entered password at login
            var hasher = new PasswordHasher<User>();
            var result = hasher.VerifyHashedPassword(user, user.PasswordHash, password);

            if (result == PasswordVerificationResult.Success)
            {
                // Store user info in session
                _logger.LogInformation("Login successful for user {Email}", email);
                HttpContext.Session.SetString("UserName", user.FirstName + " " + user.LastName);
                HttpContext.Session.SetString("UserRole", user.IsAdmin ? "Admin" : "User");
                HttpContext.Session.SetString("IsAdmin", user.IsAdmin.ToString());

                return RedirectToAction("Index", "Dashboard");
            }
        }
        _logger.LogWarning("Login failed. User not found: {Email}", email);
        ViewBag.Error = "Invalid email or password";
        return View();
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }
}
