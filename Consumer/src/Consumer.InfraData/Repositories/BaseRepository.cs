using MongoDB.Driver;
using System;

namespace Consumer.InfraData.Repositories
{
    public abstract class BaseRepository<TEntity, TModel, TKey>
    {
        private const string Database = "TransactionQuery";
        private readonly IMongoClient _mongoClient;
        private readonly string _collection;

        protected BaseRepository(IMongoClient mongoClient, IClientSessionHandle clientSessionHandle, string collection)
        {
            (_mongoClient, SessionHandle, _collection) = (mongoClient, clientSessionHandle, collection);

            if (!_mongoClient.GetDatabase(Database).ListCollectionNames().ToList().Contains(collection))
            {
                throw new ArgumentException($"There is no collection named {collection}");
            }
        }

        protected virtual IMongoCollection<TModel> Collection =>
            _mongoClient.GetDatabase(Database).GetCollection<TModel>(_collection);

        protected IClientSessionHandle SessionHandle { get; }
    }
}
