using Microsoft.EntityFrameworkCore;

namespace TodoList_Base.Database
{

    public class ApplicationDbContext : DbContext
    {
        private static IConfiguration _configuration;
        private static string _ConnectionString = "";

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration ?? throw new InvalidOperationException("Connection string not found.");
            _ConnectionString = _configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string not found.");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TodoItem>(entity =>
            {
                entity.Property(item => item.Title)
                      .IsRequired(true);
                entity.Property(item => item.Description)
                      .IsRequired(false);
                entity.Property(item => item.IsCompleted)
                      .HasDefaultValue(false);
                entity.Property(e => e.CreatedDate)
                                .HasDefaultValueSql("GETUTCDATE()");
            });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<TodoItem> TodoItems { get; set; }

        public static ApplicationDbContext CreateInstance()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(_ConnectionString);
            return new ApplicationDbContext(optionsBuilder.Options, _configuration);
        }
    }
}