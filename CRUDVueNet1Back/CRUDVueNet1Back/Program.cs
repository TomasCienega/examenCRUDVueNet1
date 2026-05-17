using CRUDVueNet1Back.Context;
using CRUDVueNet1Back.Services.Contratos;
using CRUDVueNet1Back.Services.Implementations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<CrudvueNet1Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("CadenaSQL")); 
});
builder.Services.AddScoped<IDepartamentoService,DepartamentoServiceImpl>();
builder.Services.AddScoped<IEmpleadoService,EmpleadoServiceImpl>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    //app.UseSwaggerUI();
    app.MapScalarApiReference(options =>
    {
        options.WithOpenApiRoutePattern("/swagger/v1/swagger.json");
        options.WithTitle("Mi API Documentation");
        options.WithTheme(ScalarTheme.Kepler);
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
