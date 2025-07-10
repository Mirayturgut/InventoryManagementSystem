using InventoryManagementSystem.Data;
using InventoryManagementSystem.Models.DTO_s.Item;
using InventoryManagementSystem.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Controller;

[ApiController]
[Route("/item")]
public class ItemController(AppDbContext context) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<ItemDto[]>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Get()
    {
        var items = context.Items
            .Include(i => i.Container)
            .ToList();

        var result = items.Select(item => new ItemDto
        {
            ContainerName = item.Container.Name,
            Name = item.Name,
            Quantity = item.Quantity

        }).ToArray();

        return Ok(result);

    }

    [HttpGet("search")]
    [ProducesResponseType(typeof(ItemDto[]), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Search([FromQuery] string query)
    {
        if (string.IsNullOrWhiteSpace(query))
            return BadRequest("Arama metni boş olamaz.");

        var items = context.Items
            .Include(i => i.Container)
            .Where(i => i.Name.ToLower().Contains(query.ToLower()))
            .ToList();

        if (!items.Any())
            return NotFound("Aranan ifadeye uygun ürün bulunamadı.");

        var result = items.Select(item => new ItemDto
        {
            Name = item.Name,
            Quantity = item.Quantity,
            ContainerName = item.Container?.Name

        }).ToList();

        return Ok(result);
        
    }
    
    [HttpGet("expired")]
    [ProducesResponseType(typeof(ItemDto[]), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetExpiredItems()
    {
        var today = DateTime.Today;

        var items = context.Items
            .Include(i => i.Container)
            .Where(i => i.ExpiryDate.HasValue && i.ExpiryDate < today)
            .ToList();

        var result = items.Select(item => new ItemDto
        {
            Name = item.Name,
            Quantity = item.Quantity,
            ContainerName = item.Container?.Name,
            ExpiryDate = item.ExpiryDate
        }).ToList();

        return Ok(result);
    }

    [HttpGet("expiring-soon")]
    public IActionResult GetExpiringSoonItems()
    {
        var today = DateTime.Today;
        var threshold = today.AddDays(7);

        var items = context.Items
            .Include(i => i.Container)
            .Where(i => i.ExpiryDate.HasValue && i.ExpiryDate >= today && i.ExpiryDate <= threshold)
            .ToList();

        var result = items.Select(item => new ItemDto
        {
            Name = item.Name,
            Quantity = item.Quantity,
            ContainerName = item.Container?.Name,
            ExpiryDate = item.ExpiryDate
        }).ToList();

        return Ok(result);
    }

    
    [HttpGet("with-expiry")]
    [ProducesResponseType(typeof(ItemDto[]), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetItemsWithExpiry()
    {
        var items = context.Items
            .Include(i => i.Container)
            .Where(i => i.ExpiryDate.HasValue)
            .ToList();

        var result = items.Select(item => new ItemDto
        {
            Name = item.Name,
            Quantity = item.Quantity,
            ContainerName = item.Container?.Name,
            ExpiryDate = item.ExpiryDate
        }).ToList();

        return Ok(result);
    }
    
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Create(ItemCreateDto dto)
    {
        var container = context.Containers.Find(dto.ContainerId);
        if (container == null)
            return BadRequest("ContainerId geçersiz.");
    
        var item = new Item
        {
            Name = dto.Name,
            Quantity = dto.Quantity,
            ContainerId = dto.ContainerId,
            Created = dto.Created
        };
    
        context.Items.Add(item);
        context.SaveChanges();
    
        return CreatedAtAction(nameof(Get), null);
    }
    
    [HttpPut("{id}/container")]
    public IActionResult AssignContainer(int id, ItemAssignContainerDto dto)
    {
        var item = context.Items.Find(id);
        if (item == null)
            return NotFound();
    
        var container = context.Containers.Find(dto.ContainerId);
        if (container == null)
            return BadRequest("Container bulunamadı.");
    
        item.ContainerId = dto.ContainerId;
        item.Updated = DateTime.Now;
    
        context.SaveChanges();
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var item = context.Items.Find(id);
        if (item == null)
            return NotFound();
    
        context.Items.Remove(item);
        context.SaveChanges();
    
        return NoContent();
    }

}