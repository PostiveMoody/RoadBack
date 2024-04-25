namespace RoadBack.Domain.Models
{
    public class Category
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }

        public bool? IsDeleted { get; set; }
    }
}
