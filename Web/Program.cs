using Web.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
    {
        builder.AllowAnyOrigin() // Allow any origin
               .AllowAnyMethod() // Allow any HTTP method (GET, POST, etc.)
               .AllowAnyHeader(); // Allow any HTTP header
    });
});

builder.Logging.ClearProviders(); // Clear existing logging providers
builder.Logging.AddConsole(); // Add console logging
builder.Logging.AddDebug(); // Add debug logging
builder.Logging.AddFilter("Microsoft", LogLevel.Warning); // Set log level for Microsoft namespaces
builder.Logging.AddFilter("System", LogLevel.Warning);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//add Application services 
builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();

app.MapControllers();
app.UseCors("AllowAllOrigins");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();