using InventoryManagementSystem.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Controllers;
[ApiController]
[Route("/item")]
public class ItemController( AppDbContext _context, UserManager<IdentityUser> userManager) : ControllerBase
{
    
}