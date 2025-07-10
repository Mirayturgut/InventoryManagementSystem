namespace InventoryManagementSystem.Models.DTO_s.Location;

public class LocationCreateDto
{
    public string? Name { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;
}