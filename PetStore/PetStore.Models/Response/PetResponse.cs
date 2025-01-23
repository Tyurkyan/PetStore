
namespace PetStore.Models.Response
{
    public class PetResponse
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public int Age { get; set; }
        public decimal Price { get; set; }
        public string OwnerId { get; set; } 
    }
}
