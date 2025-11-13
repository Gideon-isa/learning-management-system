using Lms.ContentDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lms.ContentDelivery.Infrastructure.Configuration
{
    public class LessonConfiguration : IEntityTypeConfiguration<LessonContent>
    {
        public void Configure(EntityTypeBuilder<LessonContent> builder)
        {
            builder.HasKey(l => l.Id);


            builder.Property(l => l.Id)
                .HasDefaultValueSql("NEWID()")
                .ValueGeneratedOnAdd();


            builder.OwnsMany(l => l.Videos, vid =>
            {
                vid.ToTable("LessonVideoContent");
                vid.WithOwner().HasForeignKey("LessonId");

                vid.HasKey("LessonId", "Title");

                vid.Property(img => img.Title).IsRequired().HasMaxLength(200);
                vid.Property(img => img.Path).IsRequired().HasMaxLength(100);
            });
        }
    }
}
