using AVStack.MessageCenter.Data.Configuration;
using AVStack.MessageCenter.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AVStack.MessageCenter.Data
{
    public class McDbContext : DbContext
    {
        public McDbContext(DbContextOptions<McDbContext> options) : base(options)
        {
        }

        public DbSet<TemplateEntity> Template { get; set; }
        public DbSet<TemplateGroupEntity> TemplateGroup { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new TemplateEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TemplateGroupEntityTypeConfiguration());
        }
    }
}