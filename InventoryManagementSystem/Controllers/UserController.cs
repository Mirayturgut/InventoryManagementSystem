using System.Net;
using System.Net.Mail;
using FluentEmail.Core;
using FluentEmail.Smtp;
using InventoryManagementSystem.Data;
using InventoryManagementSystem.Models.DTO_s.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Controllers;

[ApiController]
[Route("Auth")]
public class UserController( AppDbContext _context, UserManager<IdentityUser> userManager) : ControllerBase
{
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult RegisterUser([FromBody] UserRegisterDto dto)
    {
        var user = new IdentityUser
        {
            UserName = dto.Username,
            Email = dto.Email,
            PasswordHash = dto.PasswordHash,
        };
        _context.Users.Add(user);
        _context.SaveChanges();
        return Ok("User Eklendi.");
    }

    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Login([FromBody] UserLoginDto dto)
    {
        var user = _context.Users.FirstOrDefault(u => u.UserName == dto.Username  && u.PasswordHash == dto.PasswordHash);
        if (user == null)
        {
            return NotFound("Kullanıcı bulunamadı.");
        }

        if (user.PasswordHash != dto.PasswordHash)
        {
            return BadRequest("Şifre hatalı.Lütfen tekrar deneyin.");
        }

        return Ok($"Giriş başarılı. Hoş geldiniz: {user.UserName}.");
    }
    [HttpPost("logout")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Logout()
    {
        return Ok("Çıkış Yapıldı.");
    }
    public string Hash(string rawData)
    {
        using var sha256 = System.Security.Cryptography.SHA256.Create();
        var bytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(rawData));
        return string.Concat(bytes.Select(b => b.ToString("x2")));
    }

    [HttpGet("email")]
    public IActionResult SendEmail()
    {
        var userEmail = userManager.GetUserName(User);
        var SmtpClient = new SmtpClient("smtp.resend.com")
        {
            Port = 587,
            Credentials = new NetworkCredential("resend", "re_9L5Da84o_2JhK4ky8BqQurdFdVjSafNoC"),
            EnableSsl = true
        };
        Email.DefaultSender = new SmtpSender(SmtpClient);
        
        var email = Email
            .From("mesaj@bilgi.codemydata.com.tr","Miray Turgut")
            .To(userEmail)
            .Subject("Min Blog")
            .Body($"Mini Blog'a Hoşgeldiniz {User.Identity.Name}.")
            .Send();
        return Ok(userEmail);
    }

}