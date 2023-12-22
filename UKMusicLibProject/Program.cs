using DAL.EntityFramework;

using Microsoft.EntityFrameworkCore;
using Services.Repository;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();
// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>
    (options => options.UseSqlServer(builder.Configuration.GetConnectionString("OnionConnections"),
    b => b.MigrationsAssembly("UKMusicLibProject")));
//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddTransient<IContractRepository,ContractRepository>();
builder.Services.AddTransient<ILoginRepository, LoginRepository>();
builder.Services.AddTransient<IConcertRepository, ConcertRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IArtistRepository, ArtistRepository>();
//builder.Services.Configure<RouteOptions>(options =>
//{
//    options.ConstraintMap.Add("area", typeof(AreaRouteConstraint));
//});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllerRoute(
//      name: "areas",
//      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
//    );
//});

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");


//app.UseEndpoints(endpoints =>
//{

//    endpoints.MapAreaControllerRoute(
//        name: "Artist",
//        areaName: "Artist",
//        pattern: "Artist/{controller=Home}/{action=Index}/{id?}"
//    );

//    endpoints.MapControllerRoute(
//        name: "areaRoute",
//        pattern: "{area:exists}/{controller}/{action}/{id?}"
//    );

//    endpoints.MapControllerRoute(
//        name: "default",
//        pattern: "{controller=Login}/{action=Login}/{id?}"
//    );

//});

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
       name: "default",
       pattern: "{controller=Login}/{action=Login}/{id?}"
   );
    //endpoints.MapAreaControllerRoute(
    //    name: "Artist",
    //    areaName: "Artist",
    //    pattern: "{controller=Artist}/{action=Index}/{id?}"
    //);

    //endpoints.MapControllerRoute(
    //    name: "areaRoute",
    //    pattern: "{area:exists}/{controller}/{action}/{id?}"
    //);

   

});

app.Run();
