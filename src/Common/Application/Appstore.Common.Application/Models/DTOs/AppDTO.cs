namespace Appstore.Common.Application.Models.DTOs
{
    public sealed record AppDTO
    {
        public int Id { get; set; }
        public string Name { get; init; }
        public double Price { get; init; }
        public string Description { get; init; }
    }
}
