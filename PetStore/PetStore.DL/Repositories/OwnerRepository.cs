
using PetStore.DL.Interfaces;
using PetStore.Models.Configurations;
using PetStore.Models.DTO;

using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace PetStore.DL.Repositories
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly IMongoCollection<Owner> _owners;

        public OwnerRepository(IOptionsMonitor<MongoDbConfiguration> mongoConfig)
        {
            var client = new MongoClient(mongoConfig.CurrentValue.ConnectionString);
            var database = client.GetDatabase(mongoConfig.CurrentValue.DatabaseName);

            _owners = database.GetCollection<Owner>($"{nameof(Owner)}s");
        }

        public List<Owner> GetAll()
        {
            return _owners.Find(_ => true).ToList();
        }

        public Owner GetById(string id)
        {
            return _owners.Find(owner => owner.Id == id).FirstOrDefault();
        }

        public void Create(Owner owner)
        {
            owner.Id = Guid.NewGuid().ToString();
            _owners.InsertOne(owner);
        }

        public void Delete(string id)
        {
            _owners.DeleteOne(owner => owner.Id == id);
        }
    }
}
