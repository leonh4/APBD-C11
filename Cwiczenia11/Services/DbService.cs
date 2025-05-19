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

    public async Task AddPrescription(CreatePrescriptionDTO prescription)
    {
        var patient = await _context.Patients.FindAsync(prescription.Patient.IdPatient);
        if (patient == null)
        {
            await _context.AddAsync<Patient>(new Patient()
            {
                IdPatient = prescription.Patient.IdPatient,
                FirstName = prescription.Patient.FirstName,
                LastName = prescription.Patient.LastName,
                BirthDate = prescription.Patient.BirthDate,
            });
            
            await _context.SaveChangesAsync();
        }
        
        var idPrescription = _context.Prescriptions.Max(p => p.IdPrescription) + 1;

        foreach (var medicament in prescription.Medicaments)
        {
            await _context.PrescriptionMedicaments.AddAsync(new PrescriptionMedicament()
            {
                IdMedicament = medicament.IdMedicament,
                IdPrescription = idPrescription,
                Dose = medicament.Dose,
                Details = medicament.Description
            });
        }
        
        await _context.SaveChangesAsync();

        await _context.Prescriptions.AddAsync(new Prescription()
        {
            IdPrescription = idPrescription,
            Date = prescription.Date,
            DueDate = prescription.DueDate,
            IdPatient = prescription.Patient.IdPatient,
            IdDoctor = prescription.Doctor.IdDoctor,
        });
        
        await _context.SaveChangesAsync();
    }

    public bool CheckDates(CreatePrescriptionDTO prescription)
    {
        return prescription.Date <= prescription.DueDate;
    }

    public async Task<bool> DoesMedicamentExist(int IdMedicament)
    {
        var medi = await _context.Medicaments.FindAsync(IdMedicament);
        return medi != null;
    }

    public bool DoesPrescriptionExceedMedicamentLimit(CreatePrescriptionDTO prescription)
    {
        return prescription.Medicaments.Count >= 10;
    }
}