namespace Appstore.Common.Application.Models.DTOs
{
    public sealed record AppDTO
    {
        public string Name { get; init; }
        public string Price { get; init; }
        public string Description { get; init; }
    }
}
