using MessagePack;

namespace PetStore.Models.DTO
{
    [MessagePackObject]
    public class Pet : ICacheItem<string>
    {
        [Key(0)]
        public string Id { get; set; } = string.Empty;

        [Key(1)]
        public string Type { get; set; } = string.Empty;

        [Key(2)]
        public int Age { get; set; }

        [Key(3)]
        public decimal Price { get; set; }

        [Key(4)]
        public string OwnerId { get; set; } = string.Empty;

        [Key(5)]
        public DateTime DateInserted { get; set; } = DateTime.UtcNow;

        public string GetKey()
        {
            return Id;
        }
    }
}
