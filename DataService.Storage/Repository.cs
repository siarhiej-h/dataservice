using System.Collections.Concurrent;
using System.Collections.Generic;
using Common.Logging;
using DataService.Core;
using DataService.Core.Entities;

namespace DataService.Storage
{
    public class Repository<TIdentity, TEntity> : IRepository<TIdentity, TEntity>
        where TIdentity : class
        where TEntity : class, IEntity<TIdentity>
    {
        private static readonly ILog Log = LogManager.GetLogger<Repository<TIdentity, TEntity>>();

        private readonly ConcurrentDictionary<TIdentity, TEntity> internalDict_ =
            new ConcurrentDictionary<TIdentity, TEntity>();

        public Repository()
        {
            Log.Debug("Repository instance created.");
        }

        public void Create(TEntity instance)
        {
            Log.DebugFormat("Adding instance with id '{0}'.", instance.GetKey());
            internalDict_.TryAdd(instance.GetKey(), instance);
        }

        public TEntity Read(TIdentity id)
        {
            Log.DebugFormat("Reading instance with id '{0}'.", id);
            return internalDict_.TryGetValue(id, out var entity) ? entity : null;
        }

        public void Update(TEntity entity)
        {
            Log.DebugFormat("Updating instance with id '{0}'.", entity.GetKey());

            TEntity oldValue;
            if (internalDict_.TryGetValue(entity.GetKey(), out oldValue))
            {
                internalDict_.TryUpdate(entity.GetKey(), entity, oldValue);
            }
        }

        public void Delete(TIdentity id)
        {
            Log.DebugFormat("Deleting instance with id '{0}'.", id);
            internalDict_.TryRemove(id, out _);
        }

        public ICollection<TEntity> ListAll()
        {
            return internalDict_.Values;
        }
    }
}
