using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Models.Entities;

public class User
{
    public int Id { get; set; }
    [Required, MaxLength(200)] 
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
}