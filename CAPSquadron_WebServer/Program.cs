using CAPSquadron_WebServer.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<IMemberService, MemberService>();
builder.Services.AddScoped<IFlightService, FlightService>();
builder.Services.AddScoped<ICadetTrackerService, CadetTrackerService>();
builder.Services.AddScoped<IFileUploadService, FileUploadService>();

builder.Services.AddScoped<IFileUploadService, FileUploadService>();
builder.Services.AddScoped<IFileHandler, AttendanceFileHandler>();
builder.Services.AddScoped<IFileHandlerFactory, FileHandlerFactory>();
builder.Services.AddLogging();
builder.Services.AddHttpClient();
builder.Services.AddScoped<ApiClient>(sp => new ApiClient(Environment.GetEnvironmentVariable("ApiClientBaseUrl"), sp.GetRequiredService<HttpClient>()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
