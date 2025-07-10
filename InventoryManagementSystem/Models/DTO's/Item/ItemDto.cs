namespace InventoryManagementSystem.Models.DTO_s.Item;

public class ItemDto
{
    public string Name { get; set; }
    public int Quantity { get; set; }
    
    public DateTime? ExpiryDate { get; set; }
    
    public string? ContainerName { get; set; }
}