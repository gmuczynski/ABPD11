using APBD10.DTOs;
using APBD10.Models;
using Microsoft.AspNetCore.Mvc;

namespace APBD10.Services
{
    public interface IAPIService
    {
        public Task<Patient> GetPatient(int id);
        public Task AddPrescription([FromBody] PrescDTO prescDto);
    }
}
