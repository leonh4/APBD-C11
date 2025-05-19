using Cwiczenia11.Data;
using Cwiczenia11.DTOs;
using Cwiczenia11.Models;
using Microsoft.EntityFrameworkCore;

namespace Cwiczenia11.Services;

public class DbService : IDbService
{
    private readonly DatabaseContext _context;

    public DbService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task AddPrescriptionAsync(CreatePrescriptionDTO prescription)
    {
        var patient = _context.Patients.ToListAsync().Result.FirstOrDefault(e => e.IdPatient == prescription.Patient.IdPatient);
        if (patient == null)
        {
            await _context.AddAsync(new Patient()
            {
                IdPatient = prescription.Patient.IdPatient,
                FirstName = prescription.Patient.FirstName,
                LastName = prescription.Patient.LastName,
                BirthDate = prescription.Patient.BirthDate,
            });

            await _context.SaveChangesAsync();
        }
        
        var newPrescription = new Prescription()
        {
            Date = prescription.Date,
            DueDate = prescription.DueDate,
            IdPatient = prescription.Patient.IdPatient,
            IdDoctor = prescription.Doctor.IdDoctor,
        };
        
        await _context.Prescriptions.AddAsync(newPrescription);
        await _context.SaveChangesAsync();

        foreach (var medicament in prescription.Medicaments)
        {
            await _context.PrescriptionMedicaments.AddAsync(new PrescriptionMedicament()
            {
                IdMedicament = medicament.IdMedicament,
                IdPrescription = newPrescription.IdPrescription,
                Dose = medicament.Dose,
                Details = medicament.Description
            });
            await _context.SaveChangesAsync();
        }

       
    }

    public bool CheckDates(CreatePrescriptionDTO prescription)
    {
        return prescription.Date <= prescription.DueDate;
    }

    public async Task<bool> DoesMedicamentExistAsync(int IdMedicament)
    {
        var medi = await _context.Medicaments.FindAsync(IdMedicament);
        return medi != null;
    }

    public bool DoesPrescriptionExceedMedicamentLimit(CreatePrescriptionDTO prescription)
    {
        return prescription.Medicaments.Count >= 10;
    }

    public async Task<GetPatientWithPrescriptionsDTO> GetPatientAsync(int idPatient)
    {
        var patient = await _context.Patients.Where(e => e.IdPatient == idPatient).
            Select(e => new GetPatientWithPrescriptionsDTO()
            {
                IdPatient = e.IdPatient,
                FirstName = e.FirstName,
                LastName = e.LastName,
                BirthDate = e.BirthDate,
                Prescriptions = e.Prescriptions.Select(p => new PrescriptionDTO()
                {
                    IdPrescription = p.IdPrescription,
                    Date = p.Date,
                    DueDate = p.DueDate,
                    Medicaments = _context.PrescriptionMedicaments.Select(pm => new MedicamentDTO()
                    {
                        IdMedicament = pm.IdMedicament,
                        Name = pm.Medicament.Name,
                        Description = pm.Medicament.Description,
                        Dose = pm.Dose,
                    }).ToList()
                }).ToList(),
                Doctor = e.Prescriptions.Where(p => p.IdPatient == idPatient).
                    Select(p => new DoctorDTO()
                {
                    IdDoctor = p.IdDoctor,
                    FirstName = p.Doctor.FirstName
                }).FirstOrDefault()
            }
        ).FirstOrDefaultAsync();

        return patient;
    }
}