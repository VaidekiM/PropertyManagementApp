using System.ComponentModel.DataAnnotations;

namespace PropertyManagementApp.DTO
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mobile Number is required")]
        [RegularExpression(@"^[0-9+\-\(\)]+$", ErrorMessage = "Mobile number can only contain digits, +, -, ( and )")]
        public string MobileNumber { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(16, MinimumLength = 8, ErrorMessage = "Password must be 8-16 characters")]
        [RegularExpression(@"^(?=.*[0-9])(?=.*[!@#$%^&*]).{8,16}$", 
        ErrorMessage = "Password must contain at least 1 number and 1 special character")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
        
        public bool IsAdmin { get; set; } 
    }
}
