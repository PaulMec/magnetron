using DB.Context;
using DB.Models;
using magnetron.Application.Interfaces;
using magnetron.Application.Service;
using magnetron.Application.Services;
using magnetron.Infrastructure.Data;
using magnetron.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configurar CORS
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

// Agregar contexto de base de datos
builder.Services.AddDbContext<FacturacionContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrar repositorios
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IInvoiceHeaderRepository, InvoiceHeaderRepository>();
builder.Services.AddScoped<IInvoiceDetailRepository, InvoiceDetailRepository>();

// Registrar servicios de aplicación
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IInvoiceHeaderService, InvoiceHeaderService>();
builder.Services.AddScoped<IInvoiceDetailService, InvoiceDetailService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Aplicar migraciones automáticamente
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<FacturacionContext>();
    context.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Usar la política de CORS configurada
app.UseCors("AllowAllOrigins");

app.UseAuthorization();
app.MapControllers();
app.Run();
