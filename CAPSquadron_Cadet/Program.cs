using CAPSquadron_Shared.Services;
using CAPSquadron_Shared.Services.Attendance;
using CAPSquadron_Shared.Services.CadetTracker;
using CAPSquadron_Shared.Services.FileHandling;
using CAPSquadron_Shared.Services.Meeting;
using CAPSquadron_Shared.Services.MemberAttribute;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddBlazorBootstrap();

builder.Services.AddHttpClient();
builder.Services.AddScoped(sp => new ApiClient(Environment.GetEnvironmentVariable("ApiClientBaseUrl"), sp.GetRequiredService<HttpClient>()));
builder.Services.AddSession();

builder.Services.AddScoped<IAttendanceSignInService, AttendanceSignInService>();
builder.Services.AddScoped<ICadetTrackerService, CadetTrackerService>();
builder.Services.AddScoped<IMemberAttributeService, MemberAttributeService>();
builder.Services.AddScoped<IMemberServcie, MemberService>();
builder.Services.AddScoped<IRetrieveDataService<Member>, MemberService>();
builder.Services.AddScoped<IAttendanceService, AttendanceService>();

builder.Services.AddScoped<IFileUploadService, FileUploadService>();
builder.Services.AddScoped<IFileUploadService, FileUploadService>();
builder.Services.AddScoped<IFileHandler, AttendanceFileHandler>();
builder.Services.AddScoped<IFileHandler, CadetTrackerFileHandler>();
builder.Services.AddScoped<IFileHandlerFactory, FileHandlerFactory>();

builder.Services.AddScoped<IValidateCadet, ValidateCadet>();
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
