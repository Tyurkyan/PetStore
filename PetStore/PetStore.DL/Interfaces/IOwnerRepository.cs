using PetStore.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetStore.DL.Interfaces
{
    public interface IOwnerRepository
    {
        List<Owner> GetAll();
        Owner GetById(string id);
        void Add(Owner owner);
        void Remove(string id);
        void Update(Owner owner);
    }
}
