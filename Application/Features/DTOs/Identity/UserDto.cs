

namespace Application.Features.DTOs.Identity
{
    public class UserDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }=string.Empty;
        public string LastName { get; set; }=string.Empty;
        public string Email { get; set; }=string.Empty;
        public string PhoneNumber { get; set; }=string.Empty;
        public DateTime? CreatedAtUtc { get; set; }
        public bool EmailConfirmed { get; set; }
        public List<string> Roles { get; set; } = new List<string>();
    }
}
