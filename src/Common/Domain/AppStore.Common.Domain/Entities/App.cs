namespace AppStore.Common.Domain.Entities
{
    public sealed class App : Entity
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
    }
}
