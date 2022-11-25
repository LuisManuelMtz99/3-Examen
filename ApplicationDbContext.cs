using _3_Examen.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace _3_Examen
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DepartamentoInquilino>()
                .HasKey(al => new { al.DepartamentoId, al.InquilinoId });
        }

        public DbSet<Departamento> Departamento { get; set; }
        public DbSet<Inquilino> Inquilino { get; set; }


        public DbSet<DepartamentoInquilino> JuegoDato { get; set; }
    }
}
