namespace AppStore.App.Core.Models.DTO
{
    public sealed record ApplicationDTO
    {
        public string Name { get; init; }
        public double Price { get; init; }
        public string Description { get; init; }
    }
}
