using AVStack.MessageCenter.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AVStack.MessageCenter.Data.Configuration
{
    public class TemplateGroupEntityTypeConfiguration : IEntityTypeConfiguration<TemplateGroupEntity>
    {
        public void Configure(EntityTypeBuilder<TemplateGroupEntity> builder)
        {
            builder.ToTable("AVTemplateGroup");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .HasMaxLength(100)
                .IsRequired();

            // builder
            //     .HasMany(p => p.Templates)
            //     .WithOne();

            // builder
            //     .Navigation(p => p.Templates)
            //     .UsePropertyAccessMode(PropertyAccessMode.Property);
        }
    }
}