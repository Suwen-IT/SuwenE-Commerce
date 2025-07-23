namespace Application.Features.DTOs.Baskets
{
    public class BasketDto
    {
        public int Id { get; set; }
        public Guid AppUserId { get; set; }
        public string AppUserName { get; set; }
        public List<BasketItemDto> BasketItems { get; set; }= new List<BasketItemDto>();

        public decimal TotalPrice { get; set; }
        public int TotalItems { get; set; }

    }
}
