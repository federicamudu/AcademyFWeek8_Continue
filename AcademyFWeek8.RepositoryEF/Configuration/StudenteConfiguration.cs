using AcademyFWeek8.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AcademyFWeek8.RepositoryEF.Configuration
{
    public class StudenteConfiguration : IEntityTypeConfiguration<Studente>
    {
        public void Configure(EntityTypeBuilder<Studente> builder)
        {
            builder.ToTable("Studente");
            builder.HasKey(s=>s.ID);

            //relazione corso 1:n
            builder.HasOne(s => s.Corso).WithMany(c => c.Studenti).HasForeignKey(s=>s.CorsoCodice);


        }
    }
}