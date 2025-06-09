

using PetStore.BL.Interfaces;
using PetStore.DL.Interfaces;
using PetStore.Models.DTO;

namespace PetStore.BL.Services
{
    public class BlOwnerPetService : IBlOwnerPetService
    {
        private readonly IPetRepository _petRepository;

        public BlOwnerPetService(IPetRepository petRepository)
        {
            _petRepository = petRepository;
        }

        public async Task<List<Pet>> GetPetByOwnerIdAsync(string ownerId)
        {
            var pets = await _petRepository.GetAllAsync();
            return pets.FindAll(pet => pet.OwnerId == ownerId);
        }
    }
}
