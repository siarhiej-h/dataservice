using System.Collections.Concurrent;
using Common.Logging;
using DataService.Core;

namespace DataService
{
    public class Repository<TIdentity, TEntity> : IRepository<TIdentity, TEntity>
        where TIdentity : class
        where TEntity : class
    {
        private static readonly ILog Log = LogManager.GetLogger<Repository<TIdentity, TEntity>>();

        private readonly ConcurrentDictionary<TIdentity, TEntity> internalDict_ =
            new ConcurrentDictionary<TIdentity, TEntity>();

        public Repository()
        {
            Log.Debug("Repository instance created.");
        }

        public void Create(TIdentity id, TEntity instance)
        {
            Log.DebugFormat("Adding instance with id '{0}'.", id);
            internalDict_.TryAdd(id, instance);
        }

        public TEntity Read(TIdentity id)
        {
            Log.DebugFormat("Reading instance with id '{0}'.", id);
            return internalDict_.TryGetValue(id, out var entity) ? entity : null;
        }

        public void Update(TIdentity id, TEntity entity)
        {
            Log.DebugFormat("Updating instance with id '{0}'.", id);

            TEntity oldValue;
            if (internalDict_.TryGetValue(id, out oldValue))
            {
                internalDict_.TryUpdate(id, entity, oldValue);
            }
        }

        public void Delete(TIdentity id)
        {
            Log.DebugFormat("Deleting instance with id '{0}'.", id);
            internalDict_.TryRemove(id, out _);
        }
    }
}
