using PetStore.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetStore.DL.Interfaces
{
    public interface IPetRepository
    {
        List<Pet> GetAll();
        Pet GetById(string id);
        void Add(Pet pet);
        void Remove(string id);
        void Update(Pet pet);
    }
}
