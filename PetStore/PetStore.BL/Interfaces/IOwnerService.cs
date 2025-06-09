using PetStore.Models.DTO;

namespace PetStore.BL.Interfaces
{
    public interface IOwnerService
    {
        Task<List<Owner>> GetAllOwnersAsync();
        Task<Owner> GetOwnerByIdAsync(string id);
        Task AddOwnerAsync(Owner owner);
        Task RemoveOwnerAsync(string id);
    }
}
