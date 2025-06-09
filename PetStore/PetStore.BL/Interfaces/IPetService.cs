using PetStore.Models.DTO;
namespace PetStore.BL.Interfaces
{
    public interface IPetService
    {
        Task<List<Pet>> GetAllPetsAsync();
        Task<Pet> GetPetByIdAsync(string id);
        Task AddPetAsync(Pet pet);
        Task RemovePetAsync(string id);
    }
}
