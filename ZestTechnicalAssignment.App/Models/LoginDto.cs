using System.ComponentModel.DataAnnotations;

namespace ZestTechnicalAssignment.App.Models
{
    public class LoginDto
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,6}$", ErrorMessage = "Please Enter Valid Email")]
        public string Email { get; set; } = string.Empty;
        [Required]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^\\da-zA-Z]).{6,20}$", ErrorMessage = "Password must be 6-20 characters with at least one uppercase, one lowercase, one number, and one special character")]
        public string Password { get; set; } = string.Empty;
    }
}
