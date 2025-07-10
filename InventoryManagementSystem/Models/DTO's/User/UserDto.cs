using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Models.DTO_s.User;

public class UserDto
{
    public string Username { get; set; }
    [Required]
    public string Email { get; set; }
}