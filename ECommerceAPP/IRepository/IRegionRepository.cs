using ECommerceAPP.DTOS;

namespace ECommerceAPP.IRepository
{
    public interface IRegionRepository
    {
        Task<IEnumerable<RegionDto>> GetAllRegionsAsync();
        Task<RegionDto> GetRegionByIdAsync(int id);
        Task<RegionDto> CreateRegionAsync(RegionDto regionDto);
        Task<RegionDto> UpdateRegionAsync(RegionDto regionDto);
        Task<bool> DeleteRegionAsync(int id);
        bool IdExists(int id);
    }
}
