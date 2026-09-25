using CafeManagement.API.Models;

namespace CafeManagement.API.Repositories
{
    public interface IRoleRepository
    {
        Task<IEnumerable<Role>> GetAllAsync();

        Task<Role?> GetByIdAsync(int id);

        Task<int> CreateAsync(Role role);

        Task<bool> UpdateAsync(Role role);

        Task<bool> DeleteAsync(int id);
    }
}
