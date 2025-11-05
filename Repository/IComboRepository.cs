using RestauranteAPI.Models;

namespace RestauranteAPI.Repositories
{
    public interface IComboRepository
    {
        Task<List<Combo>> GetAllAsync();
        Task<Combo?> GetByIdAsync(int id);
        Task AddAsync(ComboCreateDto dto);
        Task UpdateAsync(int id, ComboCreateDto dto);
        Task DeleteAsync(int id);
    }
}
