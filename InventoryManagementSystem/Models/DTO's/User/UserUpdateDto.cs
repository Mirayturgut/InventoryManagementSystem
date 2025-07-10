using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Models.DTO_s.User;

public class UserUpdateDto
{
    public int Id { get; set; }
    [Required]
    public string Username { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string PasswordHash { get; set; }
    public DateTime Updated { get; set; } =  DateTime.Now;
}