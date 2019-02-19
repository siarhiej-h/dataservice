using System;

namespace DataService.Core
{
    public interface IRepository<in TIdentity, TEntity>
    {
        void Create(TIdentity id, TEntity instance);

        TEntity Read(TIdentity id);

        void Update(TIdentity id, TEntity entity);

        void Delete(TIdentity id);
    }
}
