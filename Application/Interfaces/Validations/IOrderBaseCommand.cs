namespace Application.Interfaces.Validations
{
    public interface IOrderBaseCommand
    {
        public Guid AppUserId { get; set; }
        public int ShippingAddressId { get; set; }
        public int? BillingAddressId { get; set; }
    }
}
