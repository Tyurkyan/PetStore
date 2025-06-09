using PetStore.BL.Interfaces;
using PetStore.DL.Interfaces;
using PetStore.Models.DTO;

namespace PetStore.BL.Services
{

    public class OwnerService : IOwnerService
    {
        private readonly IOwnerRepository _ownerRepository;

        public OwnerService(IOwnerRepository ownerRepository)
        {
            _ownerRepository = ownerRepository;
        }

        public Task<List<Owner>> GetAllOwnersAsync() => _ownerRepository.GetAllAsync();

        public Task<Owner> GetOwnerByIdAsync(string id) => _ownerRepository.GetByIdAsync(id);

        public Task AddOwnerAsync(Owner owner) => _ownerRepository.CreateAsync(owner);

        public Task RemoveOwnerAsync(string id) => _ownerRepository.DeleteAsync(id);
    }
}
