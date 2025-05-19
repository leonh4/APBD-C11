using System.ComponentModel.DataAnnotations;
using Cwiczenia11.Models;

namespace Cwiczenia11.DTOs;

public class CreatePrescriptionDTO
{
    [Required]
    public PatientDto Patient { get; set; }
    
    [Required]
    public List<MedicamentDto> Medicaments { get; set; }
    
    [Required]
    public DateOnly Date { get; set; }
    
    [Required]
    public DateOnly DueDate { get; set; }
    
    [Required]
    public DoctorDto Doctor { get; set; }
    
}

public class PatientDto
{
    [Required]
    public int IdPatient { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string LastName { get; set; }
    
    [Required]
    public DateOnly BirthDate { get; set; }
}

public class MedicamentDto
{
    [Required]
    public int IdMedicament { get; set; }
    [Required]
    public int Dose { get; set; }
    [Required]
    [MaxLength(100)]
    public string Description { get; set; }
}

public class DoctorDto
{
    [Key]
    public int IdDoctor { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string LastName { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Email { get; set; }
}
