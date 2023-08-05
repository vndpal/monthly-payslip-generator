using FluentValidation;
using FluentValidation.AspNetCore;
using PaySlipGenerator.Interfaces;
using PaySlipGenerator.Models;
using PaySlipGenerator.Services;
using PaySlipGenerator.Validations;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<IPaySlipInformation, PaySlipInformation>();
builder.Services.AddTransient<ITaxTableGenerator, TaxTableGenerator>();
builder.Services.AddTransient<ITaxCalculator, TaxCalculator>();

builder.Services.AddControllers().AddFluentValidation(options =>
{
    // Validate child properties and root collection elements
    options.ImplicitlyValidateChildProperties = true;
    options.ImplicitlyValidateRootCollectionElements = true;

    // Automatic registration of validators in assembly
    options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("corsapp");
app.UseAuthorization();

app.MapControllers();

app.Run();
