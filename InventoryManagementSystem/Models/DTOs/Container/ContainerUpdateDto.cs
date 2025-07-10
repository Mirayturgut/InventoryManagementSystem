namespace InventoryManagementSystem.Models.DTOs.Container;

public class ContainerUpdateDto
{
    public string Name { get; set; }
    public int LocationId { get; set; }
   
    public DateTime Updated  { get; set; } = DateTime.Now;
}
