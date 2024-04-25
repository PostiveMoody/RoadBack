namespace RoadBack.Domain.Models
{
    public class Account
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public ICollection<Expense>? Expenses { get; set; }

        public bool? IsDeleted { get; set; }
    }
}
