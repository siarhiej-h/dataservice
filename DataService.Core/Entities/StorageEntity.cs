using System;

namespace DataService.Core.Entities
{
    public sealed class StorageEntity <TIdentity, TEntity>
        where TIdentity : class
        where TEntity : class
    {
        public StorageEntity(TIdentity id, TEntity entity)
        {
            this.Id = id ?? throw new ArgumentNullException(nameof(id));
            this.Entity = entity ?? throw new ArgumentNullException(nameof(entity));
        }

        public TIdentity Id { get; set; }

        public TEntity Entity { get; set; }

    }
}
