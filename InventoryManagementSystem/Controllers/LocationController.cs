using InventoryManagementSystem.Data;
using InventoryManagementSystem.Models.DTO_s.Container;
using InventoryManagementSystem.Models.DTO_s.Location;
using InventoryManagementSystem.Models.DTOs.Container;
using InventoryManagementSystem.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Controllers;
[ApiController]
[Route("/location")]
public class LocationController( AppDbContext _context, UserManager<IdentityUser> userManager) : ControllerBase
{
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetLocation(int id)
    {
        var location = _context.Locations.Include(c => c.Containers)
            .FirstOrDefault(l => l.Id == id);
        if (location == null)
            return NotFound();
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

    [HttpPost("/location")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult CreateLocation(LocationCreateDto dto)
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
        {
            return Unauthorized("Kullanıcı kimliği alınamadı.");
        }
        
        var location = new Location()
        {
            Name = dto.Name,
            UserId = userId,
            Created = DateTime.Now,
        };
        _context.Locations.Add(location);
        _context.SaveChanges();
        
        return CreatedAtAction(nameof(GetContainers), new { id = location.Id }, null);
    }
    
}