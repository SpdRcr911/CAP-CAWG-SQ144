using CAPSquadron_WebServer.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<IMemberService, MemberService>();
builder.Services.AddScoped<IFlightService, FlightService>();
builder.Services.AddScoped<ICadetTrackerService, CadetTrackerService>();

builder.Services.AddScoped(sp =>
{
    var webAPIBaseAddress = "http://localhost:5203/";
    var httpClient = new HttpClient { BaseAddress = new Uri(webAPIBaseAddress) };
    return new ApiClient("http://localhost:5203/", httpClient); // Change the base URL as necessary
});

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
