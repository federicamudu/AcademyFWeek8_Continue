﻿using AcademyFWeek8.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AcademyFWeek8.RepositoryEF.Configuration
{
    public class DocenteConfiguration : IEntityTypeConfiguration<Docente>
    {
        public void Configure(EntityTypeBuilder<Docente> builder)
        {
            builder.ToTable("Docente");
            builder.HasKey(d => d.ID);

            //relazione con lezioni
            builder.HasMany(d=>d.Lezioni).WithOne(l=>l.Docente).HasForeignKey(l=>l.DocenteID);
        }
    }
}