namespace APBD10.Models;

public class Medicament
{
    public int IdMedicament { get; set; }
    public string Name { get; set; } = null!; 
    public string Description { get; set; } = null!; 
    public string Type { get; set; } = null!; 
    public virtual List<PrescriptionMedicament> PrescriptionMedicaments { get; set; } = new List<PrescriptionMedicament>();
}