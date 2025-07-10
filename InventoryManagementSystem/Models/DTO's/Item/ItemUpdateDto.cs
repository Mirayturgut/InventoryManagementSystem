namespace InventoryManagementSystem.Models.DTO_s.Item;

public class ItemUpdateDto
{
    public string Name { get; set; }
    public string Quantity { get; set; } //miktar
    public int ContainerId { get; set; }
    
    
    public DateTime Updated { get; set; }
}