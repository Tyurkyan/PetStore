using PetStore.DL.Cache;
using PetStore.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetStore.DL.Interfaces
{
    public interface IOwnerRepository : ICacheRepository<string,Owner>
    {
        Task<List<Owner>> GetAllAsync();
        Task<Owner> GetByIdAsync(string id);
        Task CreateAsync(Owner owner);
        Task DeleteAsync(string id);
    }
}
