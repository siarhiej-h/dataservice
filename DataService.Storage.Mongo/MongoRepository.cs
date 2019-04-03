using System;
using System.Collections.Generic;
using System.Linq;
using DataService.Core;
using DataService.Core.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace DataService.Storage.Mongo
{
    public class MongoRepository<TIdentity, TEntity> : IRepository<TIdentity, TEntity>
        where TIdentity : class, IEquatable<TIdentity>
        where TEntity : class
    {
        private IMongoCollection<StorageEntity<TIdentity, TEntity>> Entities { get; }

        static MongoRepository()
        {
            BsonClassMap.RegisterClassMap<StorageEntity<TIdentity, TEntity>>(cm =>
            {
                cm.MapIdProperty(i => i.Id);
                cm.MapProperty(i => i.Entity);
            });
        }

        public MongoRepository(string connectionString, string dbName = "DataService")
        {
            var client = new MongoClient(connectionString);
            var name = typeof(TEntity).Name;
            var db = client.GetDatabase(dbName);

            Entities = db.GetCollection<StorageEntity<TIdentity, TEntity>>(name);
        }

        public void Create(TIdentity id, TEntity instance)
        {
            var entity = new StorageEntity<TIdentity, TEntity>(id, instance);
            Entities.InsertOne(entity);
        }

        public void Delete(TIdentity id)
        {
            var filterBuilder = new FilterDefinitionBuilder<StorageEntity<TIdentity, TEntity>>();
            var filter = filterBuilder.Eq(i => i.Id, id);
            Entities.DeleteOne(filter);
        }

        public ICollection<TEntity> ListAll()
        {
            var filter = new BsonDocument();
            var options = new FindOptions
            {
                MaxTime = TimeSpan.FromMilliseconds(1000)
            };

            return Entities.Find(filter, options)
                .ToEnumerable()
                .Select(i => i.Entity)
                .ToList();
        }

        public TEntity Read(TIdentity id)
        {
            var filterBuilder = new FilterDefinitionBuilder<StorageEntity<TIdentity, TEntity>>();
            var filter = filterBuilder.Eq(i => i.Id, id);

            return Entities.FindSync(filter)
                .FirstOrDefault()
                .Entity;
        }

        public void Update(TIdentity id, TEntity entity)
        {
            var filterBuilder = new FilterDefinitionBuilder<StorageEntity<TIdentity, TEntity>>();
            var filter = filterBuilder.Eq(i => i.Id, id);
            var newEntity = new StorageEntity<TIdentity, TEntity>(id, entity);
            Entities.FindOneAndReplace(filter, newEntity);
        }
    }
}
