using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetStore.Models.DTO
{
    public class Pet
    {
        public string Id { get; set; }
        public string Type { get; set; } 
        public int Age { get; set; }    
        public decimal Price { get; set; }
        public string OwnerId { get; set; }
    }
}
