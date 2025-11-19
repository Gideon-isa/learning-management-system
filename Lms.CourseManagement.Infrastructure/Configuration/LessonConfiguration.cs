using Lms.CourseManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lms.CourseManagement.Infrastructure.Configuration
{
    public class LessonConfiguration : IEntityTypeConfiguration<Content>
    {
        public void Configure(EntityTypeBuilder<Content> builder)
        {
            builder.HasKey(l => l.Id);


            builder.Property(l => l.Id)
                .HasDefaultValueSql("NEWID()")
                .ValueGeneratedOnAdd();


            builder.OwnsMany(l => l.Notes, notes =>
            {
                notes.ToTable("LessonNotes");
                notes.WithOwner().HasForeignKey("LessonId"); // foreign key for Content on Notes Table

                notes.HasKey("LessonId", "Title"); // Composite Key
                                                   
                notes.Property(n => n.Content).IsRequired();
                notes.Property(n => n.Title).HasMaxLength(50);
            });

            builder.OwnsMany(l => l.Images, images =>
            {
                images.ToTable("LessonImages");
                images.WithOwner().HasForeignKey("LessonId");

                images.HasKey("LessonId", "Caption");

                images.Property(img => img.Path).IsRequired().HasMaxLength(200);
                images.Property(img => img.Caption).IsRequired().HasMaxLength(100);
            });

            builder.OwnsMany(l => l.Tags, tag =>
            {
                tag.ToTable("LessonTagReference");
                tag.WithOwner().HasForeignKey("LessonId");
                tag.HasKey("LessonId", "TagId");
                tag.Property(t => t.TagName).IsRequired().HasMaxLength(200);
                tag.Property(t => t.TagId).HasMaxLength(200);
            });

            builder.OwnsMany(l => l.Videos, vid =>
            {
                vid.ToTable("LessonVideo");
                vid.WithOwner().HasForeignKey("LessonId");
    
                vid.HasKey("LessonId", "Title");

                vid.Property(img => img.Title).IsRequired().HasMaxLength(200);
                vid.Property(img => img.Path).IsRequired().HasMaxLength(100);
            });
        }
    }
}
