namespace Application.Interfaces.Validations
{
    public interface IReviewCommandBase
    {
        public int ProductId { get; set; }
        public Guid AppUserId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
    }
}
