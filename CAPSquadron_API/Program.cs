using CAPSquadron_API.Data;
using CAPSquadron_API.Models;
using CAPSquadron_API.Schemas;
using CAPSquadron_API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo 
    {
        Version = "v1",
        Title = "CAP Squadron 144 API",
        Description = "APIs for viewing information cadet squadron 144"
    });

    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments((string?)Path.Combine(AppContext.BaseDirectory, xmlFile));

    options.SchemaFilter<SwaggerSchemaFilter>();
});

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

// Register services
builder.Services.AddScoped<ICsvParsingService, CsvParsingService>();
builder.Services.AddScoped<IXlsxParsingService, XlsxParsingService>();
builder.Services.AddScoped<IRetrieveDataService<Member>, MemberService>();
builder.Services.AddScoped<IProcessDataService<MemberCsvModel>, MemberService>();
builder.Services.AddScoped<IRetrieveDataService<Achievement>, AchievementService>();
builder.Services.AddScoped<IProcessDataService<AchievementCsvModel>, AchievementService>();
builder.Services.AddScoped<IFlightService, FlightService>();
builder.Services.AddScoped<ICadetTrackerService, CadetTrackerService>();

var connectionString = Environment.GetEnvironmentVariable("DefaultConnection") ?? builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));
builder.Services.AddDbContext<QueryDbContext>(options =>
    options.UseNpgsql(connectionString));
    
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
