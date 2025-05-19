using Cwiczenia11.DTOs;
using Cwiczenia11.Models;

namespace Cwiczenia11.Services;

public interface IDbService
{
    Task AddPrescriptionAsync(CreatePrescriptionDTO prescription);
    
    bool CheckDates(CreatePrescriptionDTO prescription);
    
    Task<bool> DoesMedicamentExistAsync(int IdMedicament);
    
    bool DoesPrescriptionExceedMedicamentLimit(CreatePrescriptionDTO prescription);
    
    Task<GetPatientWithPrescriptionsDTO> GetPatientAsync(int idPatient);
}