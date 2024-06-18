
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RepositoryPatternApi.Application.Interfaces.IRepositories;
using RepositoryPatternApi.Application.Interfaces.IServices;
using RepositoryPatternApi.Domain.Entities;
using RepositoryPatternApi.Infrastructure.Implementations.Services;
using RepositoryPatternApi.Persistence.Implementations.Repositories;
using RepositoryPatternAPI;
using RepositoryPatternAPI.Data;
using RepositoryPatternAPI.Repositories;
using RepositoryPatternAPI.Services.Abstractions;
using RepositoryPatternAPI.Services.Implementations;
using RepositoryPatternAPI.UnitofWork;
using RepositoryPatternAPI.UnitOfWork;
using Serilog;
using System.Net;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).Enrich.FromLogContext().CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger); 
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("V1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "V1",
        Title = "WebApi",
        Description = "ProductWebApi"
    });

    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Name = "Authorization",
        Description = "Bearer Authentication with JWT Bearer",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Id = "Bearer",
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }
    });
});

builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddDbContext<ApplicationDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// Configure Identity services and DbContext
builder.Services.AddIdentity<User, Role>()
    .AddEntityFrameworkStores<ApplicationDBContext>()
    .AddDefaultTokenProviders();

//builder.Services.AddDbContext<ApplicationDBContext>(options =>
//options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
//.UseLazyLoadingProxies()); // Enable lazy loading proxies
// Register repositories
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<AuthorRepository>();
builder.Services.AddScoped<BookRepository>();

// Register Unit of Work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(o =>
{
    var Key = Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]);
    o.SaveToken = true;
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Key)
    };
});
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/V1/swagger.json", "Library Web API");
    });
}


app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
