using Cwiczenia11.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cwiczenia11.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientsController : ControllerBase
{
    private readonly DbService _dbService;

    public PatientsController(DbService dbService)
    {
        _dbService = dbService;
    }
    
    
}