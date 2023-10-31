using Hangfire;
using Hangfire.Storage.SQLite;
using HangfireBasicAuthenticationFilter;
using HangfireExample.Server.Services;
using HangfireExample.Server.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IMailService, MailService>();

// Hangfire Client
builder.Services.AddHangfire(config => config
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSQLiteStorage(builder.Configuration.GetConnectionString("HangfireConnection"))
);

// Hangfire Server
builder.Services.AddHangfireServer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();
app.MapControllers();

app.UseHangfireDashboard("/hangfire");
app.MapHangfireDashboard(new DashboardOptions
{
    DashboardTitle = "All the services",
    Authorization = new[]
    {
        new HangfireCustomBasicAuthenticationFilter
        {
            Pass = builder.Configuration.GetSection("HangfireSettings:Password").Value,
            User = builder.Configuration.GetSection("HangfireSettings:UserName").Value,
        }
    }
});

app.MapFallbackToFile("index.html");

app.Run();