namespace Application.Interfaces.Validations;

public interface IAddressCommandBase
{
   public string Title { get; set; }
   public string Country { get; set; }
   public string City { get; set; }
   public string State { get; set; }
   public string Street { get; set; }
   public string ZipCode { get; set; }
   public bool IsDefault { get; set; }
   public Guid AppUserId { get; set; }
}