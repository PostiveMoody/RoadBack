using Microsoft.EntityFrameworkCore;
using RoadBack.Domain.Models;

namespace RoadBack.DAL
{
    public class ExpenseTrackerDbContext : DbContext
    {
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Account> Accounts { get; set; }
 
        public ExpenseTrackerDbContext()
        {
        }

        public ExpenseTrackerDbContext(DbContextOptions<ExpenseTrackerDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=ExpenseTracker;Integrated Security=True;Trust Server Certificate=True");
        }
    }
}
