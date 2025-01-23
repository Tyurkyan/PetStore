using PetStore.Models.DTO;

namespace PetStore.BL.Interfaces
{
    public interface IOwnerService
    {
        List<Owner> GetAllOwners();
        Owner GetOwnerById(string id);
        void AddOwner(Owner owner);
        void RemoveOwner(string id);
    }
}
