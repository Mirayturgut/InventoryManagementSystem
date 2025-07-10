namespace InventoryManagementSystem.Models.DTOs.Item;

public class ItemDto
{
    public string Name { get; set; }
    public int Quantity { get; set; }
    public string ContainerName { get; set; }
    public DateTime? ExpiryDate {get; set;} //SKT
}