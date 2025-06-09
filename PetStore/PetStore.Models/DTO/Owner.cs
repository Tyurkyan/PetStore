using MessagePack;

namespace PetStore.Models.DTO
{
    [MessagePackObject]
    public class Owner : ICacheItem<string>
    {
        [Key(0)]
        public string Id { get; set; } = string.Empty;

        [Key(1)]
        public string Name { get; set; } = string.Empty;

        [Key(2)]
        public string PhoneNumber { get; set; } = string.Empty;

        [Key(3)]
        public DateTime DateInserted { get; set; } = DateTime.UtcNow;

        public string GetKey()
        {
            return Id;
        }
    }
}
