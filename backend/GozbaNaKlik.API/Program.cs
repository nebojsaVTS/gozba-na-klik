using GozbaNaKlik.API.Data;
using GozbaNaKlik.API.Repositories;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

builder.Services.AddScoped<IRestaurantRepository, RestaurantRepository>();

WebApplication app = builder.Build();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();