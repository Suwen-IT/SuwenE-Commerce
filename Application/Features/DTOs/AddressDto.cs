namespace Application.Features.DTOs.Addresses
{
    public class AddressDto
    {
        public int Id { get; set; }
        public Guid AppUserId { get; set; } 
        public string Title { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty; 
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
        public bool IsDefault { get; set; } 
        
        public string FullAddress { get; set; } = string.Empty; 
    }
}
