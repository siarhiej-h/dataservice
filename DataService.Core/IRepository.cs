using System;
using System.Collections.Generic;

namespace DataService.Core
{
    public interface IRepository<in TIdentity, TEntity>
        where TIdentity : class, IEquatable<TIdentity>
        where TEntity: class
    {
        void Create(TIdentity id, TEntity instance);

        TEntity Read(TIdentity id);

        void Update(TIdentity id, TEntity entity);

        void Delete(TIdentity id);

        ICollection<TEntity> ListAll();
    }
}
