using CAPSquadron_Shared.Services;
using CAPSquadron_Shared.Services.CadetTracker;
using CAPSquadron_Shared.Services.Meeting;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddBlazorBootstrap();

builder.Services.AddHttpClient();
builder.Services.AddScoped(sp => new ApiClient(Environment.GetEnvironmentVariable("ApiClientBaseUrl"), sp.GetRequiredService<HttpClient>()));
builder.Services.AddSession();

builder.Services.AddScoped<IValidateCadet, ValidateCadet>();
builder.Services.AddScoped<IMemberServcie, MemberService>();
builder.Services.AddScoped<IRetrieveDataService<Member>, MemberService>();
builder.Services.AddScoped<ICadetTrackerService, CadetTrackerService>();
builder.Services.AddScoped<IMeetingService, MeetingService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
