using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Models.DTO_s.User;

public class UserLoginDto
{
    [Required]
    public string Username { get; set; }
    [Required]
    public string PasswordHash { get; set; }
}