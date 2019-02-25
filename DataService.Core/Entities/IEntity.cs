using System;
using System.Collections.Generic;
using System.Text;

namespace DataService.Core.Entities
{
    public interface IEntity<out TKey>
    {
        TKey GetKey();
    }
}
