namespace InventoryManagementSystem.Models.DTOs.Container;

public class ContainerDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int LocationId { get; set; }
    public string LocationName { get; set; }
}