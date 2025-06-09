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

        public async Task<List<Pet>> GetAllAsync()
        {
            var result = await _pets.FindAsync(_ => true);
            return await result.ToListAsync();
        }

        public async Task<Pet> GetByIdAsync(string id)
        {
            var result = await _pets.FindAsync(pet => pet.Id == id);
            return await result.FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Pet pet)
        {
            pet.Id = Guid.NewGuid().ToString();
            await _pets.InsertOneAsync(pet);
        }

        public async Task DeleteAsync(string id)
        {
            await _pets.DeleteOneAsync(pet => pet.Id == id);
        }


        public async Task<IEnumerable<Pet>> DifLoad(DateTime lastExecuted)
        {
            var result = await _pets.FindAsync(pet => pet.DateInserted >= lastExecuted);
            return await result.ToListAsync();
        }

        public async Task<IEnumerable<Pet>> FullLoad()
        {
            return await GetAllAsync();
        }
    }
}
