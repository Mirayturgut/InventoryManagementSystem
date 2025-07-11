using System.Security.Claims;
using InventoryManagementSystem.Data;
using InventoryManagementSystem.Models.DTO_s.Container;
using InventoryManagementSystem.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Controllers;
[ApiController]
[Route("[controller]")]
[Authorize]
public class ContainerController( AppDbContext _context, UserManager<IdentityUser> userManager) : ControllerBase
{
    [HttpGet("{id}/items")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetContainer(int id)
    {
        var container = _context.Containers.Include(c => c.Items)
            .FirstOrDefault(x => x.Id == id);
        if (container == null)
        {
            return NotFound();
        }
        return Ok(container);
    }

    [HttpPost("create")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult CreateContainer([FromBody] ContainerCreateDto dto)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
        {
            return Unauthorized("Giriş yapılmamış.");
        }
      
        var container = new Container
        {
            Name = dto.Name,
            LocationId = dto.LocationId,
            UserId = userId
        };
        _context.Containers.Add(container);
        _context.SaveChanges();
        return Ok(container);
    }

    [HttpPut("update")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult UpdateContainer([FromBody] ContainerUpdateDto dto)
    {
        var container = _context.Containers.Find(dto.Name);
        var location =  _context.Locations.Find(dto.LocationId);
        
        if (container == null || location == null) return NotFound();
        container.Name = dto.Name;
        container.LocationId = dto.LocationId;
        container.Updated = DateTime.Now;
        
        _context.Containers.Update(container);
        _context.SaveChanges();
        return Ok(container);
    }
}