using MicroCore.Catalog.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MicroCore.Catalog.API.Data.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories"); 

        builder.HasKey(x => x.Id); 

        builder.Property(x => x.Name)
               .IsRequired()
               .HasMaxLength(100);

        builder.HasMany(x => x.Courses)
               .WithOne(x => x.Category)
               .HasForeignKey(x => x.CategoryId)
               .OnDelete(DeleteBehavior.Cascade); 
    }
}