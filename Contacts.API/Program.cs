

using Contacts.Services;
using Contacts.Infrastructure;
using Contacts.Core;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Contacts.Services.Profiles;

var builder = WebApplication.CreateBuilder(args);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging();
builder.Services.AddControllers();
builder.Services.AddCors();

builder.Services.AddAutoMapper(cfg =>
    cfg.AddProfile<MapperConfig>()
);

builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddBusinessServices();
builder.Services.AddAuthorization();

builder.Services.AddLogging();
builder.Services.AddApplicationInsightsTelemetry();

var app = builder.Build();
  
app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true) // allow any origin 
    .AllowCredentials());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/error");
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
