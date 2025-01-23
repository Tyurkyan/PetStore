using PetStore.DL.Interfaces;
using PetStore.Models.Configurations;
using PetStore.Models.DTO;
using Microsoft.Extensions.Options;
using MongoDB.Driver;


namespace PetStore.DL.Repositories
{
    public class PetRepository : IPetRepository
    {
        private readonly IMongoCollection<Pet> _pets;

        public PetRepository(IOptionsMonitor<MongoDbConfiguration> mongoConfig)
        {
            var client = new MongoClient(mongoConfig.CurrentValue.ConnectionString);
            var database = client.GetDatabase(mongoConfig.CurrentValue.DatabaseName);

            _pets = database.GetCollection<Pet>($"{nameof(Pet)}s");
        }

        public List<Pet> GetAll()
        {
            return _pets.Find(_ => true).ToList();
        }

        public Pet GetById(string id)
        {
            return _pets.Find(pet => pet.Id == id).FirstOrDefault();
        }

        public void Create(Pet pet)
        {
            pet.Id = Guid.NewGuid().ToString();
            _pets.InsertOne(pet);
        }

        public void Delete(string id)
        {
            _pets.DeleteOne(pet => pet.Id == id);
        }

    }
}
