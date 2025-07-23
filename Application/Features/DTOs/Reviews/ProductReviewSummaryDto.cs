namespace Application.Features.DTOs.Reviews
{
    public class ProductReviewSummaryDto
    {
        public int ProductId { get; set; }
        public double AverageRating { get; set; }
        public int TotalReviews { get; set; }
    }
}
