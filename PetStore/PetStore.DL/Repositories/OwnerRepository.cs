
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

        public async Task<List<Owner>> GetAllAsync()
        {
            var result = await _owners.FindAsync(_ => true);
            return await result.ToListAsync();
        }

        public async Task<Owner> GetByIdAsync(string id)
        {
            var result = await _owners.FindAsync(owner => owner.Id == id);
            return await result.FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Owner owner)
        {
            owner.Id = Guid.NewGuid().ToString();
            await _owners.InsertOneAsync(owner);
        }

        public async Task DeleteAsync(string id)
        {
            await _owners.DeleteOneAsync(owner => owner.Id == id);
        }

        public async Task<IEnumerable<Owner>> DifLoad(DateTime lastExecuted)
        {
            var result = await _owners.FindAsync(owner => owner.DateInserted >= lastExecuted);
            return await result.ToListAsync();
        }

        public async Task<IEnumerable<Owner>> FullLoad()
        {
            return await GetAllAsync();
        }
    }
}
