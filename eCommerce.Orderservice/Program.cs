using eCommerce.Orderservice.BuisnessLogicLayer;
using eCommerce.Orderservice.BuisnessLogicLayer.Mappers;
using eCommerce.Orderservice.BuisnessLogicLayer.Validators;
using eCommerce.Orderservice.dataLayer;
using eCommerce.Orderservice.Middleware;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDataLayer(builder.Configuration);
builder.Services.AddBLLayer(builder.Configuration);

builder.Services.AddControllers().AddJsonOptions(op=>op.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.AddCors(op => { op.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});
//Add AutoMapper profiles
builder.Services.AddAutoMapper(typeof(OrderAddRequestToOrderMappingProfile));

//Add Swagger documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Fluent Validation Registrations
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssembly(typeof(OrderAddRequestValidator).Assembly);

var app = builder.Build();
app.UseExceptionHandlerMiddleware();
app.UseRouting();
app.UseCors();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseSwagger();
app.UseSwaggerUI();
app.MapGet("/", () => "Hello World!");
//Endpoints
app.MapControllers();

app.Run();
