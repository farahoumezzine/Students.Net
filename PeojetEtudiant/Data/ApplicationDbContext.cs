using Microsoft.EntityFrameworkCore;
using PeojetEtudiant.Models;

namespace PeojetEtudiant.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Etudiants> Etudiants { get; set; } // Correction: Etudiant au lieu de Etudiants
        public DbSet<Classse> Classes { get; set; } // Correction: Classe au lieu de Classse

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuration des relations et contraintes
            modelBuilder.Entity<Etudiants>()
                .HasOne(e => e.Classe)
                .WithMany(c => c.Etudiants)
                .HasForeignKey(e => e.ClasseId);

            modelBuilder.Entity<Classse>()
                .HasKey(c => c.Id_c); // Spécification de la clé primaire
        }
    }
}