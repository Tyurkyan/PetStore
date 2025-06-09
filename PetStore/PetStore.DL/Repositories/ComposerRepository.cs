﻿using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PetStore.DL.Cache;
using PetStore.Models.Configurations;
using PetStore.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetStore.DL.Repositories
{
    internal class ComposerRepository : ICacheRepository<int, Composer>
    {
        private readonly IMongoCollection<Composer> _actorsCollection;
        private readonly ILogger<ComposerRepository> _logger;

        public ComposerRepository(ILogger<ComposerRepository> logger, IOptionsMonitor<MongoDbConfiguration> mongoConfig)
        {
            _logger = logger;

            if (string.IsNullOrEmpty(mongoConfig?.CurrentValue?.ConnectionString) || string.IsNullOrEmpty(mongoConfig?.CurrentValue?.DatabaseName))
            {
                _logger.LogError("MongoDb configuration is missing");

                throw new ArgumentNullException("MongoDb configuration is missing");
            }

            var client = new MongoClient(mongoConfig.CurrentValue.ConnectionString);
            var database = client.GetDatabase(mongoConfig.CurrentValue.DatabaseName);

            _actorsCollection = database.GetCollection<Composer>($"{nameof(Composer)}s");
        }


        public async Task<IEnumerable<Composer?>> DifLoad(DateTime lastExecuted)
        {
            var result = await _actorsCollection.FindAsync(m => m.DateInserted >= lastExecuted);

            return await result.ToListAsync();
        }

        public async Task<IEnumerable<Composer?>> FullLoad()
        {
            return await GetAllActors();
        }

        public async Task<IEnumerable<Composer?>> GetAllActors()
        {
            var result = await _actorsCollection.FindAsync(m => true);

            return await result.ToListAsync();
        }

        public async Task<Composer?> GetById(int id)
        {
            var result = await _actorsCollection.FindAsync(m => m.Id == id);

            return await result.FirstOrDefaultAsync();
        }
    }
}
