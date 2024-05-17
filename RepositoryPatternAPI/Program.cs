    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Options;
    using RepositoryPatternApi.Application.Interfaces.IRepositories;
    using RepositoryPatternApi.Application.Interfaces.IServices;
    using RepositoryPatternApi.Infrastructure.Implementations.Services;
    using RepositoryPatternApi.Persistence.Implementations.Repositories;
    using RepositoryPatternAPI;
    using RepositoryPatternAPI.Data;
    using RepositoryPatternAPI.Repositories;
    using RepositoryPatternAPI.Services.Abstractions;
    using RepositoryPatternAPI.Services.Implementations;
    using RepositoryPatternAPI.UnitofWork;
    using RepositoryPatternAPI.UnitOfWork;

    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddAutoMapper(typeof(MappingProfile));
    builder.Services.AddDbContext<ApplicationDBContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
    //builder.Services.AddDbContext<ApplicationDBContext>(options =>
    //options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
    //               sqlOptions => sqlOptions.MigrationsAssembly("RepositoryPatterunAPI.Infrastructure")));

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


    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
