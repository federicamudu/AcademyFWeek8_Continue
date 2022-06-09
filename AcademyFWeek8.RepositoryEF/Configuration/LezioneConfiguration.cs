using AcademyFWeek8.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AcademyFWeek8.RepositoryEF.Configuration
{
    public class LezioneConfiguration : IEntityTypeConfiguration<Lezione>
    {
        public void Configure(EntityTypeBuilder<Lezione> builder)
        {
            builder.ToTable("Lezione");
            builder.HasKey(l => l.LezioneId);

            //relazione docente
            builder.HasOne(l=>l.Docente).WithMany(d=>d.Lezioni).HasForeignKey(l=>l.DocenteID);

            //relazione corsi
            builder.HasOne(l=>l.Corso).WithMany(c=>c.Lezione).HasForeignKey(l=>l.CorsoCodice);
        }
    }
}