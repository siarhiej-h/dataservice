using System.Collections.Generic;
using DataService.Core.Entities;

namespace DataService.Core
{
    public interface IRepository<in TIdentity, TEntity>
        where TEntity: IEntity<TIdentity>
    {
        void Create(TEntity instance);

        TEntity Read(TIdentity id);

        void Update(TEntity entity);

        void Delete(TIdentity id);

        ICollection<TEntity> ListAll();
    }
}
