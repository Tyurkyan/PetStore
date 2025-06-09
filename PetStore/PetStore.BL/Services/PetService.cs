using PetStore.BL.Interfaces;
using PetStore.DL.Interfaces;
using PetStore.Models.DTO;

namespace PetStore.BL.Services
{
    public class PetService : IPetService
    {
        private readonly IPetRepository _petRepository;

        public PetService(IPetRepository petRepository)
        {
            _petRepository = petRepository;
        }

        public Task<List<Pet>> GetAllPetsAsync() => _petRepository.GetAllAsync();

        public Task<Pet> GetPetByIdAsync(string id) => _petRepository.GetByIdAsync(id);

        public Task AddPetAsync(Pet pet) => _petRepository.CreateAsync(pet);

        public Task RemovePetAsync(string id) => _petRepository.DeleteAsync(id);
    }
}
