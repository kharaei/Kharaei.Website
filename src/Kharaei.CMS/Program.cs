using Kharaei.CMS.Middlewares;
using Kharaei.CMS.Models;
using OrchardCore.Logging;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOrchardCms();
builder.Host.UseNLogHost();

builder.Services.Configure<SiteSettings>(builder.Configuration.GetSection(nameof(SiteSettings)));
var origins = builder.Configuration.GetValue<string>("Origins");
if (string.IsNullOrWhiteSpace(origins))
    throw new ArgumentNullException("Origin property has not been set!(appsettings issue)");

builder.Services.AddCors(options => 
{
    options.AddPolicy(name: "MyCors", x => 
        x.WithOrigins(origins.Split(','))
        .AllowCredentials()
        .AllowAnyMethod()
        .AllowAnyHeader());
});
        

var app = builder.Build();
app.UseMiddleware<RedirectMiddleware>();
app.UseCors("MyCors");
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseOrchardCore();
app.Run();
