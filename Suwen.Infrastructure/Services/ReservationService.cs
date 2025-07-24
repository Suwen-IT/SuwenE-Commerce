using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Suwen.Infrastructure.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReadRepository<ProductAttributeValue> _productAttributeValueReadRepository;
        private readonly IWriteRepository<ProductAttributeValue> _productAttributeValueWriteRepository;
        private readonly IReadRepository<BasketItem> _basketItemReadRepository;
        private readonly IWriteRepository<BasketItem> _basketItemWriteRepository;

        public ReservationService( IReadRepository<ProductAttributeValue> productAttributeValueReadRepository, IWriteRepository<ProductAttributeValue> productAttributeValueWriteRepository,
            IReadRepository<BasketItem> basketItemReadRepository, IWriteRepository<BasketItem> basketItemWriteRepository)
        {
            _productAttributeValueReadRepository = productAttributeValueReadRepository;
            _productAttributeValueWriteRepository = productAttributeValueWriteRepository;
            _basketItemReadRepository = basketItemReadRepository;
            _basketItemWriteRepository = basketItemWriteRepository;
        }
        public async Task<int> ReleaseExpiredReservationsAsync()
        {
            var expiredItems = await _basketItemReadRepository.Query()
                .Where(bi => bi.ReservationExpirationDate.HasValue &&
                             bi.ReservationExpirationDate.Value < DateTime.UtcNow)
                .Include(bi => bi.ProductAttributeValue)
                .ToListAsync();

            int releasedCount = 0;

            foreach (var item in expiredItems)
            {
                if (item.ProductAttributeValue != null)
                {
                    item.ProductAttributeValue.ReservedStock =
                        Math.Max(0, item.ProductAttributeValue.ReservedStock - item.Quantity);

                    await _productAttributeValueWriteRepository.UpdateAsync(item.ProductAttributeValue);
                    await _basketItemWriteRepository.DeleteAsync(item);
                    releasedCount++;
                }
            }

            await _productAttributeValueWriteRepository.SaveChangesAsync();
            await _basketItemWriteRepository.SaveChangesAsync();

            return releasedCount;

        }

        public async Task<bool> ReleaseStockAsync(int productAttributeValueId, int quantity)
        {
            var pav = await _productAttributeValueReadRepository.GetByIdAsync(productAttributeValueId);
            if(pav == null)
                return false;

            pav.ReservedStock=Math.Max(0, pav.ReservedStock - quantity);

            var updated= await _productAttributeValueWriteRepository.UpdateAsync(pav);
            if (updated == null)
                return false;

            return await _productAttributeValueWriteRepository.SaveChangesAsync();
        }

        public async Task<bool> ReserveStockAsync(int productAttributeValueId, int quantity)
        {
            var pav = await _productAttributeValueReadRepository.GetByIdAsync(productAttributeValueId);
            if (pav == null)
                return false;

            int availableStock = pav.Stock - pav.ReservedStock;

            if (availableStock < quantity)
                return false;

            pav.ReservedStock += quantity;

            var result = await _productAttributeValueWriteRepository.UpdateAsync(pav);
            if (result == null)
                return false;

            return await _productAttributeValueWriteRepository.SaveChangesAsync();
        }

        public async Task<bool> UpdateReservationAsync(int productAttributeValueId, int oldQuantity, int newQuantity)
        {
            var pav = await _productAttributeValueReadRepository.GetByIdAsync(productAttributeValueId);
            if (pav == null)
                return false;

            int diff = newQuantity - oldQuantity;

            if (diff > 0 && (pav.Stock - pav.ReservedStock < diff))
                return false;

            pav.ReservedStock += diff;

            var result = await _productAttributeValueWriteRepository.UpdateAsync(pav);
            if (result == null)
                return false;

            return await _productAttributeValueWriteRepository.SaveChangesAsync();

        }
    }
}
