using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using storehouse_backend.Models;

namespace storehouse_backend.Services
{
    public class ProductService
    {
        private readonly IMongoCollection<Product> storeCollection ;
        public ProductService(IOptions<MongoDBSettings> dbSettings)
        {
            MongoClient client = new MongoClient(dbSettings.Value.ConnectionString);
            IMongoDatabase database = client.GetDatabase(dbSettings.Value.DatabaseName);
            storeCollection = database.GetCollection<Product>(dbSettings.Value.CollectionName);
        }

        public async Task<List<Product>> GetAsync() {
            var filter = Builders<Product>.Filter.Empty;
            return await storeCollection.Find(filter).ToListAsync();
        }
        public async Task<Product?> GetAsync(string Id)
        {
            return await storeCollection.Find(product => product.Id == Id).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Product product)
        {
            await storeCollection.InsertOneAsync(product);
        }

        public async Task UpdateAsync(string Id, Product product)
        {
            await storeCollection.ReplaceOneAsync(x => x.Id == Id, product);
        }
        public async Task RemoveAsync(string Id) => await storeCollection.DeleteOneAsync(x => x.Id == Id);
    }
}
