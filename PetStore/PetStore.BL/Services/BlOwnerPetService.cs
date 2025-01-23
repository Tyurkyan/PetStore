

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

        public List<Pet> GetPetByOwnerId(string ownerId)
        {
            return _petRepository.GetAll().Where(pet => pet.OwnerId == ownerId).ToList();
        }
    }
}
