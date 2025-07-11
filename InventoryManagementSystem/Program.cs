using System.Net;
using System.Net.Mail;
using InventoryManagementSystem.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using InventoryManagementSystem;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});


builder.Services.AddIdentityApiEndpoints<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddAuthorization();

builder.Services.AddControllers();

builder.Services.AddOpenApi();

var smtpSettings = builder.Configuration.GetSection("SmtpSettings").Get<SmtpSettings>();
builder.Services
    .AddFluentEmail(smtpSettings.FromEmail, smtpSettings.FromName)
    .AddSmtpSender(new SmtpClient(smtpSettings.Host, smtpSettings.Port)
    {
        EnableSsl = true,
        Credentials = new NetworkCredential(smtpSettings.Username, smtpSettings.Password),
    });

var app = builder.Build();

app.UseCors("AllowFrontend");

app.MapOpenApi();
app.MapControllers();

app.MapScalarApiReference();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapGroup("Auth").MapIdentityApi<IdentityUser>().WithTags("Auth");

app.Run();