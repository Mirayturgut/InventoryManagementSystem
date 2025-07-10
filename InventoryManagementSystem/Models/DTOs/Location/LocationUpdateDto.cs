namespace InventoryManagementSystem.Models.DTOs.Location;

public class LocationUpdateDto
{
    public string Name { get; set; }
    public DateTime Updated { get; set; } = DateTime.Now;
}