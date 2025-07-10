namespace InventoryManagementSystem.Models.DTO_s.Container;

public class ContainerCreateDto
{
    public string Name { get; set; }
    
    public int LocationId { get; set; }
    public string LocationName { get; set; }
    
    public DateTime Created { get; set; } = DateTime.Now;
}