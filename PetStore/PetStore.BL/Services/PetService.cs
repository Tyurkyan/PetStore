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

        public List<Pet> GetAllPets() => _petRepository.GetAll();

        public Pet GetPetById(string id) => _petRepository.GetById(id);

        public void AddPet(Pet pet) => _petRepository.Create(pet);

        public void RemovePet(string id) => _petRepository.Delete(id);

    }
}
