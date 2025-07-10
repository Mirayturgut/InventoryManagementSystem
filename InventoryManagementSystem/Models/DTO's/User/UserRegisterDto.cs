using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Models.DTO_s.User;

public class UserRegisterDto
{
    [Required]
    public string Username { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string PasswordHash { get; set; }
}