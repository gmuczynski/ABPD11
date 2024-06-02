using APBD10.Models;

namespace APBD10.DTOs;

public class PrescDTO
{
    public int IdPrescription { get; set; }
    public Doctor doctor { get; set; }
    public ICollection<Medicament> meds { get; set; } = new List<Medicament>();
    public DateOnly Date { get; set; }
    public DateOnly DueDate { get; set; }
    public Patient patient { get; set; }
}