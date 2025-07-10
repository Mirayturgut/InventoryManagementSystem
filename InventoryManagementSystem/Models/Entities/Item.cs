using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace InventoryManagementSystem.Models.Entities;

public class Item
{
    public int Id { get; set; }
    [Required, MaxLength(200)] 
    public string Name { get; set; } = string.Empty;
    [Required]
    public int Quantity { get; set; }
    public int ContainerId { get; set; }
    public Container Container { get; set; }
     
    public DateTime? ExpiryDate { get; set; } //nullable olabilir
     
    public DateTime Created { get; set; } = DateTime.Now;
    public DateTime Updated { get; set; }
    
    public string UserId { get; set; }
    public IdentityUser User { get; set; }
}