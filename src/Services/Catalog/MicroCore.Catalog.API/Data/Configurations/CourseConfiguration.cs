using MicroCore.Catalog.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MicroCore.Catalog.API.Data.Configurations;

public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.ToTable("Courses"); 

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(x => x.Description)
               .HasMaxLength(1000);

        builder.Property(x => x.Price)
               .HasColumnType("decimal(18,2)");

        builder.Property(x => x.ImageUrl)
               .HasMaxLength(200);

        builder.OwnsOne(c => c.Feature, feature =>
        {
            feature.Property(x => x.Duration).HasColumnName("Feature_Duration");
            feature.Property(x => x.Rating).HasColumnName("Feature_Rating");
            feature.Property(x => x.EducatorFullName)
                   .HasColumnName("Feature_EducatorFullName")
                   .HasMaxLength(100);
        });
    }
}