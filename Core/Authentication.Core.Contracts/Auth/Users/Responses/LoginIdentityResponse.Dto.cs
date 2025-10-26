namespace Authentication.API.DTOs
{
    public class LoginIdentityResponseDto
    {
        public Guid Uid { get; set; }
        public string Email { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName => $"{FirstName} {LastName}";

        public string Token { get; set; } = string.Empty;
    }
}
