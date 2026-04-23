using Microsoft.EntityFrameworkCore;
using Pingvinas.Event.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddHttpContextAccessor();
builder.Services.AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddPingvinasServices()
    .AddLogging();

builder.Services.AddDbContext<Pingvinas.Event.Domain.Context.EventContext>(
        options => options.UseInMemoryDatabase("PingvinasEventDb")
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
