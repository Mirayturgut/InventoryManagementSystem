namespace InventoryManagementSystem.Models.DTOs.Location;

public class LocationCreateDto
{
    public string Name { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;
}