using PetStore.DL.Cache;
using PetStore.Models.DTO;

namespace PetStore.DL.Interfaces
{
    public interface IPetRepository : ICacheRepository<string,Pet>
    {
        Task<List<Pet>> GetAllAsync();
        Task<Pet> GetByIdAsync(string id);
        Task CreateAsync(Pet pet);
        Task DeleteAsync(string id);
    }
}
