namespace InventoryManagementSystem.Models.DTO_s.Container;

public class ContainerUpdateDto
{
    public string? Name { get; set; } 
    
    public int LocationId { get; set; }
    public string LocationName { get; set; }
    
    public DateTime Updated { get; set; } 
}