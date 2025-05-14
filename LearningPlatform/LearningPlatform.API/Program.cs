using LearningPlatform.API.Extensions;
using LearningPlatform.API.Middlewares;
using LearningPlatform.Application.Interfaces;
using LearningPlatform.Application.Interfaces.Auth;
using LearningPlatform.Application.Interfaces.Repositories;
using LearningPlatform.Application.Services;
using LearningPlatform.Persistance;
using LearningPlatform.Persistence;
//using LearningPlatform.Persistence.Repositories;
using LearningPlatform.Infrastructure;
using Microsoft.AspNetCore.CookiePolicy;
//using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using LearningPlatform.Persistence.Repositories;
using LearningPlatform.Core.Enums;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;

services.AddApiAuthentication(configuration);

services.AddEndpointsApiExplorer();

services.AddSwaggerGen();

services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));
services.Configure<AuthorizationOptions>(configuration.GetSection(nameof(AuthorizationOptions)));

services.AddTransient<ExceptionMiddleware>();

services.AddDbContext<LearningDbContext>(options =>
{
    //options.UseNpgsql(configuration.GetConnectionString(nameof(LearningDbContext)));
    options.UseSqlServer(builder.Configuration.GetConnectionString("LearningDbContext"));
});

services.AddScoped<IJwtProvider, JwtProvider>();
services.AddScoped<IPasswordHasher, PasswordHasher>();

services.AddScoped<ICourseRepository, CourseRepository>();
services.AddScoped<ILessonsRepository, LessonsRepository>();
services.AddScoped<IUsersRepository, UsersRepository>();

services.AddScoped<CoursesService>();
services.AddScoped<LessonsService>();
services.AddScoped<UserService>();

services.AddAutoMapper(typeof(DataBaseMappings));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
    HttpOnly = HttpOnlyPolicy.Always,
    Secure = CookieSecurePolicy.Always
});

app.UseAuthentication();

app.UseAuthorization();

app.AddMappedEndpoints();

app.MapGet("get", () =>
{
    return Results.Ok("ok");
}).RequirePermissions(Permission.Read);

app.MapPost("post", () =>
{
    return Results.Ok("ok");
}).RequirePermissions(Permission.Create);

app.MapPut("put", () =>
{
    return Results.Ok("ok");
}).RequirePermissions(Permission.Update);

app.MapDelete("delete", () =>
{
    return Results.Ok("ok");
}).RequirePermissions(Permission.Delete);

app.Run();
