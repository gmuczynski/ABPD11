using APBD10.DTOs;
using APBD10.Models;
using APBD10.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APBD10.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ApiController : ControllerBase
{

    private readonly IAPIService _apiService;

    public ApiController(APIService apiService) {  this._apiService = apiService; }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPatient(int id)
    {
        return Ok(await _apiService.GetPatient(id));
    }
    
    [HttpPost]
    public async Task<IActionResult> AddPrescription([FromBody] PrescDTO prescDto)
    {
        await _apiService.AddPrescription(prescDto);
        return Created();
    }
}