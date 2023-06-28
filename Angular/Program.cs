using EmployeeManagement.Bll.Dto;
using EmployeeManagement.Bll.Mapping;
using EmployeeManagement.Bll.Services;
using EmployeeManagement.Bll.Validators;
using EmployeeManagement.Common.Exceptions;
using EmployeeManagement.Common.RequestContext;
using EmployeeManagement.Dal.Repository;
using EmployeeManager.Api.RequestContext;
using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Dal;
using AutoMapper;
using EmployeeManagement.Api;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);
var env = builder.Environment;

// DbContext
builder.Services.AddDbContext<EmployeeManagementDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

// Services
builder.Services.AddScoped(typeof(ILogicService<>), typeof(LogicService<>));
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();

// Request context
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IRequestContext, HttpRequestContext>();

// Validation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddScoped<IValidator<EmployeeCreateDto>, EmployeeCreateDtoValidator>();
builder.Services.AddScoped<IValidator<EmployeeUpdateDto>, EmployeeUpdateDtoValidator>();
builder.Services.AddScoped<IValidator<DepartmentCreateOrUpdateDto>, DepartmentCreateOrUpdateDtoValidator>();
builder.Services.AddScoped<IValidator<LoginDto>, LoginDtoValidator>();

// Automapper
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<EmployeeProfile>();
    cfg.AddProfile<DepartmentProfile>();
});

// Exception handling, Problem details
builder.Services.AddProblemDetails();
builder.Services.Configure<ExceptionHandlerOptions>(options =>
{
    options.AllowStatusCode404Response = true;
    options.ExceptionHandler = async context =>
    {
        context.Response.ContentType = "application/problem+json";

        if (context.RequestServices.GetService<IProblemDetailsService>() is { } problemDetailsService)
        {
            var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
            var exceptionType = exceptionHandlerFeature?.Error;

            if (exceptionType is not null)
            {
                (string Title, string Detail, int StatusCode) details = exceptionType switch
                {
                    HttpResponseException httpResponseException =>
                    (
                        exceptionType.GetType().Name,
                        exceptionType.Message,
                        context.Response.StatusCode = (int)httpResponseException.StatusCode
                    ),
                    _ =>
                    (
                        exceptionType.GetType().Name,
                        exceptionType.Message,
                        context.Response.StatusCode = StatusCodes.Status500InternalServerError
                    )
                };

                var problemDetails = new ProblemDetails
                {
                    Title = details.Title,
                    Detail = details.Detail,
                    Status = details.StatusCode
                };
                await context.Response.WriteAsJsonAsync(problemDetails);
            }
        }
    };
});

// JWT auth
var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("super secret key"));
builder.Services
    .AddAuthentication(opt =>
    {
        opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = key,
            ValidateAudience = false,
            ValidateIssuer = false
        };
    });

builder.Services.AddControllers();
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    scope.ServiceProvider.GetRequiredService<IMapper>().ConfigurationProvider.AssertConfigurationIsValid();
    var dbContext = scope.ServiceProvider.GetRequiredService<EmployeeManagementDbContext>();

    dbContext.Database.EnsureDeleted();
    dbContext.Database.EnsureCreated();

    await SeedData.AddSeedData(dbContext);
}

app.UseStatusCodePages();
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthorization();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
