
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerceAPP.DTOS;
using ECommerceAPP.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce_Project.Repository
{
    public class TerritoryRepository : ITerritoryRepository
    {
        private readonly Zecommerce123Context _context;

        public TerritoryRepository(Zecommerce123Context context)
        {
            _context = context;
        }

        public async Task<TerritoryDto> CreateTerritory(TerritoryDto territoryDto)
        {
            var territory = new Territory
            {
                TerritoryId = territoryDto.TerritoryID,
                TerritoryDescription = territoryDto.TerritoryDescription,
                RegionId = territoryDto.RegionID
            };

            _context.Territories.Add(territory);
            await _context.SaveChangesAsync();

            return new TerritoryDto
            {
                TerritoryID = territory.TerritoryId,
                TerritoryDescription = territory.TerritoryDescription,
                RegionID = territory.RegionId ?? 0
            };
        }

        public async Task<List<TerritoryDto>> GetAllTerritory()
        {
            return await _context.Territories
                .Select(t => new TerritoryDto
                {
                    TerritoryID = t.TerritoryId,
                    TerritoryDescription = t.TerritoryDescription,
                    RegionID = t.RegionId ?? 0
                })
                .ToListAsync();
        }

        public async Task<TerritoryDto> GetTerritoryById(string id)
        {
            var territory = await _context.Territories.FindAsync(id);
            if (territory == null)
            {
                return null;
            }

            return new TerritoryDto
            {
                TerritoryID = territory.TerritoryId,
                TerritoryDescription = territory.TerritoryDescription,
                RegionID = territory.RegionId ?? 0
            };
        }

        public async Task<TerritoryDto> UpdateTerritory(string id, TerritoryDto territoryDto)
        {
            var territory = await _context.Territories.FindAsync(id);
            if (territory == null)
            {
                return null;
            }

            territory.TerritoryDescription = territoryDto.TerritoryDescription;
            territory.RegionId = territoryDto.RegionID;

            await _context.SaveChangesAsync();

            return new TerritoryDto
            {
                TerritoryID = territory.TerritoryId,
                TerritoryDescription = territory.TerritoryDescription,
                RegionID = territory.RegionId ?? 0
            };
        }

        public async Task<bool> DeleteTerritoryById(string id)
        {
            var territory = await _context.Territories.FindAsync(id);
            if (territory == null)
            {
                return false;
            }

            _context.Territories.Remove(territory);
            await _context.SaveChangesAsync();

            return true;
        }

        public bool IdExist(string id)
        {
            return _context.Territories.Any(t => t.TerritoryId == id);
        }
    }
}