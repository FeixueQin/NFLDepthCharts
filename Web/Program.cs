using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Filters;
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

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ContractResolver = new DefaultContractResolver();
        options.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented; // Optional for pretty-printing JSON
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.ExampleFilters();
});
builder.Services.AddSwaggerExamplesFromAssemblyOf<Program>();


//add Application services 
builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();

app.MapControllers();
app.UseCors("AllowAllOrigins");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
    });
}

app.UseHttpsRedirection();

app.Run();