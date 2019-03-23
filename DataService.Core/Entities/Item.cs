namespace DataService.Core.Entities
{
    public class Item
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
    }
}
