using APBD10.Models;
using APBD10.Models.AuthModels;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace APBD10;

public class DbContext: Microsoft.EntityFrameworkCore.DbContext
{
    public DbContext()
    {
        
    }

    public DbContext(DbContextOptions<DbContext> options)
        : base(options)
    {
    }
    
    public virtual DbSet<Patient> Patients { get; set; }
    public virtual DbSet<Prescription> Prescriptions { get; set; }
    public virtual DbSet<Medicament> Medicaments { get; set; }
    public virtual DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }
    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<User> Users { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(
            "Data Source=localhost;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False"
            );

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Patient>(entity =>
        {
            entity.ToTable("Patient");
            
            entity.HasKey(e => e.IdPatient).HasName("Pat_pk");
            
            entity.Property(e => e.IdPatient).ValueGeneratedNever();
            entity.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Birthdate).IsRequired();
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.ToTable("Doctor");
            
            entity.HasKey(e => e.IdDoctor).HasName("Doc_pk");
            
            entity.Property(e => e.IdDoctor).ValueGeneratedNever();
            entity.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
        });

        modelBuilder.Entity<Prescription>(entity =>
        {
            entity.ToTable("Prescription");
            
            entity.HasKey(e => e.IdPrescription).HasName("Pre_pk");
            entity.Property(e => e.IdPrescription).ValueGeneratedNever();

            entity.Property(e => e.Date).IsRequired();
            entity.Property(e => e.DueDate).IsRequired();

            entity.HasOne<Doctor>()
                .WithMany(e => e.Prescriptions)
                .HasForeignKey(e => e.IdDoctor)
                .HasConstraintName("Prescription_Doctor")
                .OnDelete(DeleteBehavior.Restrict);
        
            entity.HasOne<Patient>()
                .WithMany(e => e.Prescriptions)
                .HasForeignKey(e => e.IdPatient)
                .HasConstraintName("Prescription_Patient")
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Medicament>(entity =>
        {
            entity.ToTable("Medicament");
            
            entity.HasKey(e => e.IdMedicament).HasName("Med_pk");
            entity.Property(e => e.IdMedicament).ValueGeneratedNever();

            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Description).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Type).IsRequired().HasMaxLength(100);
        });

        modelBuilder.Entity<PrescriptionMedicament>(entity =>
        {
            entity.ToTable("Prescription_Medicament");
            
            entity.HasKey(e => new { e.IdPrescription, e.IdMedicament }).HasName("PreMed_pk");

            entity.Property(e => e.Details).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Dose).IsRequired();

            entity.HasOne<Medicament>()
                .WithMany(e => e.PrescriptionMedicaments)
                .HasForeignKey(e => e.IdMedicament)
                .HasConstraintName("PrescriptionMedicament_Medicament")
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne<Prescription>()
                .WithMany(e => e.PrescriptionMedicaments)
                .HasForeignKey(e => e.IdPrescription)
                .HasConstraintName("PrescriptionMedicament_Prescriptions")
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}