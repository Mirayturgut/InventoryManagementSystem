namespace InventoryManagementSystem.Models.DTOs.Container;

public class ContainerCreateDto
{
    public string Name { get; set; }
    public int LocationId { get; set; }
   
    public DateTime Created { get; set; } = DateTime.Now; 
}