using System;
using System.Collections.Generic;
using System.Text;

namespace DataService.Core.Entities
{
    public class Item : IEntity<string>
    {
        public Item()
        {
        }

        public Item(string name, decimal price)
        {
            Name = name;
            Price = price;
        }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string GetKey() => Name;
    }
}
