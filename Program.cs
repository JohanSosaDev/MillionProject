using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PropertyApi.Config;
using PropertyApi.Mappings;
using PropertyApi.Middleware;
using PropertyApi.Repositories;
using PropertyApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Settings
builder.Services.Configure<MongoSettings>(builder.Configuration.GetSection("Mongo"));
builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoSettings>>().Value;
    return new MongoClient(settings.ConnectionString);
});
builder.Services.AddSingleton(sp =>
{
    var client = sp.GetRequiredService<IMongoClient>();
    var settings = sp.GetRequiredService<IOptions<MongoSettings>>().Value;
    return client.GetDatabase(settings.Database);
});

// Services
builder.Services.AddScoped<IPropertyRepository, PropertyRepository>();
builder.Services.AddScoped<IPropertyService, PropertyService>();
builder.Services.AddAutoMapper(typeof(PropertyProfile).Assembly);

// API
builder.Services.AddControllers();

// CORS
builder.Services.AddCors(o =>
    o.AddPolicy("AllowFrontend", p => p
        .WithOrigins("http://localhost:5173")
        .AllowAnyHeader()
        .AllowAnyMethod()
    )
);

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Global error handling
app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseCors("AllowFrontend");
app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
