namespace InventoryManagementSystem.Models.DTOs.Item;

public class ItemUpdateDto
{
    public string Name { get; set; }
    public int Quantity { get; set; }
    public int ContainerId { get; set; }
    public DateTime Updated { get; set; } = DateTime.Now;
}