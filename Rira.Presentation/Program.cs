using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Rira.Application.Interfaces;
using Rira.Grpc;
using Rira.Infrastructure.Persistence;
using Rira.Infrastructure.UserService;
using Rira.Presentation.Interceptors;
using Rira.Presentation.Services;
using Rira.Presentation.Validators;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));



builder.Services.AddGrpc(options =>
{
    options.Interceptors.Add<ExceptionInterceptor>();
    options.Interceptors.Add<ValidatorInterceptor>();
    options.EnableDetailedErrors = true;
});

builder.Services.AddGrpcReflection();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, Rira.Application.Services.UserService>();

builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

var app = builder.Build();

app.MapGrpcService<UserGrpcService>();

if (app.Environment.IsDevelopment())
{
    app.MapGrpcReflectionService();
}

app.Run();
