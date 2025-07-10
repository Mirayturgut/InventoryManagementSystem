namespace InventoryManagementSystem.Models.DTO_s.Item;

public class ItemCreateDto
{
    public string Name { get; set; }
    public int Quantity { get; set; } //miktar
    public int ContainerId { get; set; }
    
    public DateTime? ExpiryDate { get; set; }   
    
    public DateTime Created { get; set; } = DateTime.Now;
    
}