using Brasserie.DTOs;
using Brasserie.Models;

namespace Brasserie.Services
{
    public interface ISaleService
    {
        Task<Sale> AddSale(CreateSaleRequest saleRequest);
    }
}