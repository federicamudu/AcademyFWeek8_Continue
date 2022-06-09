using AcademyFWeek8.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AcademyFWeek8.RepositoryEF.Configuration
{
    public class CorsoConfiguration : IEntityTypeConfiguration<Corso>
    {
        public void Configure(EntityTypeBuilder<Corso> builder)
        {
            builder.ToTable("Corso");
            builder.HasKey(c => c.CorsoCodice);
            builder.Property(c => c.Nome).IsRequired();
            builder.Property(c => c.Descrizione).IsRequired(false);

            //relazione studente 1:n
            builder.HasMany(c=>c.Studenti).WithOne(s=>s.Corso).HasForeignKey(s=>s.CorsoCodice);
            //relazione lezione
            builder.HasMany(c => c.Lezione).WithOne(l => l.Corso).HasForeignKey(l => l.CorsoCodice);




        }
    }
}