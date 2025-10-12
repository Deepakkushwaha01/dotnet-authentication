using System.ComponentModel.DataAnnotations;

namespace Authentication.API.DTOs
{
    public class CreateAdminRequestDto
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Admin level is required.")]
        [Range(1, 5, ErrorMessage = "Admin level must be between 1 and 5.")]
        public byte AdminLevel { get; set; }

        [StringLength(200, ErrorMessage = "Business address cannot exceed 200 characters.")]
        public string BusinessAddress { get; set; } = string.Empty;

        public bool IsVerified { get; set; } = false;

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 100 characters.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone number is required.")]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        required public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters.")]
        public string FirstName { get; set; } = string.Empty;

        [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters.")]
        public string LastName { get; set; } = string.Empty;
    }
}
