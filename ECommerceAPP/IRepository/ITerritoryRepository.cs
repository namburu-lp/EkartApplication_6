using System.Collections.Generic;
using System.Threading.Tasks;
using ECommerceAPP.DTOS;

namespace ECommerce_Project.Repository
{
    public interface ITerritoryRepository
    {
        Task<TerritoryDto> CreateTerritory(TerritoryDto territoryDto);
        Task<List<TerritoryDto>> GetAllTerritory();
        Task<TerritoryDto> GetTerritoryById(string id);
        Task<TerritoryDto> UpdateTerritory(string id, TerritoryDto territoryDto);
        Task<bool> DeleteTerritoryById(string id);
        bool IdExist(string id);
    }
}
