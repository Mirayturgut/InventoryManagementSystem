using InventoryManagementSystem.Data;
using InventoryManagementSystem.Models.DTO_s.Container;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Controllers;
[ApiController]
[Route("/location")]
public class LocationController( AppDbContext _context, UserManager<IdentityUser> userManager) : ControllerBase
{
    [HttpGet("{id}/items")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetContainer(int id)
    {
        var location = _context.Locations
            .FirstOrDefault(x => x.Id == id);
        if (location == null)
        {
            return NotFound();
        }
        return Ok(location);
    }

    [HttpGet("{id}/containers")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetContainers(int id)
    {
        var location = _context.Locations.Include(l => l.Containers)
            .FirstOrDefault(x => x.Id == id);
        if (location == null) return NotFound();

        var result = location.Containers.Select(c => new ContainerDto
        {
            Name = c.Name,
            LocationId = c.Id,
        }).ToList();
        return Ok(result);
    }
    
}