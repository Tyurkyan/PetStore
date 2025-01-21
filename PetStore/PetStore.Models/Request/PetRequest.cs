using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetStore.Models.Request
{
    public class PetRequest
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public int Age { get; set; }
        public decimal Price { get; set; }
    }
}
