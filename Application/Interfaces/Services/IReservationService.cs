namespace Application.Interfaces.Services
{
    public interface IReservationService
    {
        Task<bool> ReserveStockAsync(int productAttributeValueId, int quantity);
        Task<bool> ReleaseStockAsync(int productAttributeValueId, int quantity);
        Task<bool> UpdateReservationAsync(int productAttributeValueId, int oldQuantity, int newQuantity);
        Task<int> ReleaseExpiredReservationsAsync();
    }
}
