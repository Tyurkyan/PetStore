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
        void Create(Owner owner);
        void Delete(string id);
    }
}
