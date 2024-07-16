using KPUserManagementAPI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using KPUserManagementAPI.BusinessLogic;

var builder = WebApplication.CreateBuilder(args);

// Configuring app configuration
builder.Configuration.AddJsonFile("appsettings.json");

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddMvc();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Connection string for local db
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHttpClient();
builder.Services.AddRazorPages();

builder.Services.AddScoped<UsersBusinessLogic>();
builder.Services.AddScoped<GroupsBusinessLogic>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseCors("AllowAllOrigins");

app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages(); 
    endpoints.MapControllers(); 
});

app.Run();
