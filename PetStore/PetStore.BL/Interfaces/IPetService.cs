using PetStore.Models.DTO;
namespace PetStore.BL.Interfaces
{
    public interface IPetService
    {
        List<Pet> GetAllPets();
        Pet GetPetById(string id);
        void AddPet(Pet pet);
        void RemovePet(string id);
    }
}
