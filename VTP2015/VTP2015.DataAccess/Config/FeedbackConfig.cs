using System.Data.Entity.ModelConfiguration;
using VTP2015.Entities;

namespace VTP2015.DataAccess.Config
{
    public class FeedbackConfig : EntityTypeConfiguration<Feedback>
    {
        public FeedbackConfig()
        {
            // Primary key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Text).IsRequired();

            // Relationships
            HasOptional(t => t.Student)
                .WithMany(t => t.Feedback)
                .HasForeignKey(d => d.StudentId);
        }

        
    }
}
