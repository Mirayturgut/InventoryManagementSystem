using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace InventoryManagementSystem.Models.Entities;

public class Location
{
    public int Id { get; set; }
    [Required,MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    public ICollection<Container> Containers { get; set; } = new List<Container>();
    
    public DateTime Created { get; set; } = DateTime.Now;
    public DateTime Updated { get; set; } 
    
    public string UserId { get; set; }
    public IdentityUser User { get; set; }
}