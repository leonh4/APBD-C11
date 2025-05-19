using Cwiczenia11.DTOs;
using Cwiczenia11.Models;
using Cwiczenia11.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cwiczenia11.Controllers;


[ApiController]
[Route("api/[controller]")]
public class PrescriptionsController : ControllerBase
{
    private readonly IDbService _dbService;

    public PrescriptionsController(IDbService dbService)
    {
        _dbService = dbService;
    }


    [HttpPost]
    public async Task<IActionResult> AddPrescription([FromBody] CreatePrescriptionDTO prescription)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (!_dbService.CheckDates(prescription)) return BadRequest("Due date has to be before date");
        foreach (MedicamentDto medicament in prescription.Medicaments)
        {
            if (! await _dbService.DoesMedicamentExist(medicament.IdMedicament)) return BadRequest($"Medicament with Id {medicament.IdMedicament} doesn't exist");
        }
        if (!_dbService.DoesPrescriptionExceedMedicamentLimit(prescription)) return BadRequest("Prescription exceeds the limit of 10 medicaments");
        
        _dbService.AddPrescription(prescription);
        return Created();
    }
}