using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using Stripe;
using Stripe.Checkout;

StripeConfiguration.ApiKey = "sk_test_51NXLakSI2vMSdrtvAxI3gcig704OgM6rqNhPhhZV48WE12UkqZMipN2HVbUrO5cDHLwersNS44MHwX8MQnmB66No0068bqPNYn";
//var app = builder.Build();
// Add services to the container.
var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllersWithViews();
}

var app = builder.Build();
{
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }
    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
}
app.Run();
