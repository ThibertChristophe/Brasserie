using Brasserie.DTOs;

namespace Brasserie.Services
{
    public interface IStockService
    {
        Task<StockDTO> GetStockByWholesalerAndBeer(long beerId, long wholesalerId);
		Task<StockDTO> Update(long idStock, StockDTO stock);
    }
}