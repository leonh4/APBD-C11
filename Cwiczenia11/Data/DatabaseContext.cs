using Cwiczenia11.Models;
using Microsoft.EntityFrameworkCore;

namespace Cwiczenia11.Data;

public class DatabaseContext : DbContext
{
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }
    
    protected DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Patient>().HasData(new List<Patient>()
        {
            new Patient() { IdPatient = 1, FirstName = "Jane", LastName = "Doe", BirthDate = new DateOnly(1981, 10, 10) },
            new Patient() { IdPatient = 2, FirstName = "John", LastName = "Doe", BirthDate = new DateOnly(1981, 10, 12) },
        });

        modelBuilder.Entity<Doctor>().HasData(new List<Doctor>()
        {
            new Doctor() { IdDoctor = 1, FirstName = "Joe", LastName = "Smith", Email = "JSDoc@mail.com"},
        });

        modelBuilder.Entity<Medicament>().HasData(new List<Medicament>()
        {
            new Medicament() { IdMedicament = 1, Name = "CoughSyrup1", Description = "Description 1", Type = "Syrup"},
            new Medicament() { IdMedicament = 2, Name = "ThroatPill1", Description = "Description 2", Type = "Pill"},
        });
    }
}