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
        public List<Owner> GetAllOwners() => _ownerRepository.GetAll();

        public Owner GetOwnerById(string id) => _ownerRepository.GetById(id);

        public void AddOwner(Owner owner) => _ownerRepository.Create(owner);

        public void RemoveOwner(string id) => _ownerRepository.Delete(id);
    }
}
