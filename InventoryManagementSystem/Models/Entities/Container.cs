using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace InventoryManagementSystem.Models.Entities;

public class Container
{
    [Key]
    public int Id { get; set; }
    [Required, MaxLength(100)]
    public string? Name { get; set; } 
    public int LocationId { get; set; }
    public Location Location { get; set; }
    public ICollection<Item> Items { get; set; } =new List<Item>();
    
    public DateTime Created { get; set; } = DateTime.Now;
    public DateTime Updated { get; set; } 
    
    public string UserId { get; set; }
    public IdentityUser User { get; set; }
}