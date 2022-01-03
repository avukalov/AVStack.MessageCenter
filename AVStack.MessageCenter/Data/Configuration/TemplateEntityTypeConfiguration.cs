using AVStack.MessageCenter.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AVStack.MessageCenter.Data.Configuration
{
    public class TemplateEntityTypeConfiguration : IEntityTypeConfiguration<TemplateEntity>
    {
        public void Configure(EntityTypeBuilder<TemplateEntity> builder)
        {
            builder.ToTable("AVTemplate");

            // Columns
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(p => p.Body)
                .HasColumnType("TEXT")
                .HasMaxLength(int.MaxValue)
                .IsRequired();

            // !!! NOTE: For some reason EF Core creates FK on his own ???

            // Relationship
            // builder
            //     .HasOne(p => p.TemplateGroup)
            //     .WithMany(p => p.Templates);

            // Navigation
            // builder
            //     .Navigation(p => p.TemplateGroup)
            //     .UsePropertyAccessMode(PropertyAccessMode.Property);

        }
    }
}