using Cwiczenia11.DTOs;
using Cwiczenia11.Models;

namespace Cwiczenia11.Services;

public interface IDbService
{
    Task AddPrescription(CreatePrescriptionDTO prescription);
    
    bool CheckDates(CreatePrescriptionDTO prescription);
    
    Task<bool> DoesMedicamentExist(int IdMedicament);
    
    bool DoesPrescriptionExceedMedicamentLimit(CreatePrescriptionDTO prescription);
}