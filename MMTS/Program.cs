using MMTS.Data;
using MMTS.Services; // Add for IMenteeService
using Microsoft.EntityFrameworkCore;
using SoapCore; // Add for SOAP support

var builder = WebApplication.CreateBuilder(args);

// Add MVC 
builder.Services.AddControllersWithViews();

// Register DbContext 
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register SOAP service 
builder.Services.AddSingleton<IMenteeService, MenteeService>(); //  service
builder.Services.AddSoapCore(); // Enable SOAP

var app = builder.Build();

// Middleware pipeline 
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// Map SOAP endpoint 
app.UseEndpoints(endpoints =>
{
    // SOAP endpoint for IMenteeService
    endpoints.UseSoapEndpoint<IMenteeService>(
        path: "/MenteeService.asmx",
        encoder: new SoapEncoderOptions(),
        serializer: SoapSerializer.DataContractSerializer
    );

    // MVC route (existing)
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();