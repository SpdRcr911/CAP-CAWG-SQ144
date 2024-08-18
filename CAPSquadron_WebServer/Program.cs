using CAPSquadron_Shared.Services;
using CAPSquadron_Shared.Services.Attendance;
using CAPSquadron_Shared.Services.CadetTracker;
using CAPSquadron_Shared.Services.FileHandling;
using CAPSquadron_Shared.Services.Flight;
using CAPSquadron_Shared.Services.Meeting;
using CAPSquadron_Shared.Services.MemberAttribute;
using CAPSquadron_Shared.Services.QualityCadetUnit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<IAttendanceSignInService, AttendanceSignInService>();
builder.Services.AddScoped<IFlightService, FlightService>();
builder.Services.AddScoped<ICadetTrackerService, CadetTrackerService>();
builder.Services.AddScoped<IQualityCadetUnitReportService, QualityCadetUnitReportService>();
builder.Services.AddScoped<IMemberAttributeService, MemberAttributeService>();
builder.Services.AddScoped<IMemberServcie, MemberService>();
builder.Services.AddScoped<IRetrieveDataService<Member>, MemberService>();
builder.Services.AddScoped<IAttendanceService, AttendanceService>();
builder.Services.AddScoped<IMeetingService, MeetingService>();

builder.Services.AddScoped<IFileUploadService, FileUploadService>();
builder.Services.AddScoped<IFileUploadService, FileUploadService>();
builder.Services.AddScoped<IFileHandler, AttendanceFileHandler>();
builder.Services.AddScoped<IFileHandler, CadetTrackerFileHandler>();
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
