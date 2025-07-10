namespace InventoryManagementSystem.Models.DTOs.Item;

public class ItemCreateDto
{
    public string Name { get; set; }
    public int Quantity { get; set; }
    public int ContainerId { get; set; }
    public DateTime? ExpiryDate { get; set; } // SKT
    public DateTime Created { get; set; } = DateTime.Now;
}