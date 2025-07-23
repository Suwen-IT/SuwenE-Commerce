namespace Application.Features.DTOs.Baskets
{
    public class BasketSummaryDto
    {
        public int BasketId { get; set; }
        public Guid AppUserId { get; set; }
        public int TotalItems { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
