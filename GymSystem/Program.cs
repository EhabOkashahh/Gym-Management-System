using GymSystem.Extension.Classes;
using GymSystem.Middlewares;
using GymSystemBLL.Extensions.Classes;
using GymSystemBLL.Extensions.Interfaces;
using GymSystemBLL.Services.Classes;
using GymSystemBLL.Services.Interfaces;
using GymSystemDAL.Data.Contexts;
using GymSystemDAL.Data.Seeding;
using GymSystemDAL.Entities;
using GymSystemDAL.Repositories.Classes;
using GymSystemDAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.Extensibility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options => options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IUnitOfWork , UnitOfWork>();
builder.Services.AddScoped<IMemberService , MemberService>();
builder.Services.AddScoped<IPlanService,PlanService>();
builder.Services.AddScoped<IMemberShipExtensionsMethods,MemberShipExtensions>();
builder.Services.AddScoped<IMemberShipService, MemberShipService>();
builder.Services.AddScoped<IMemberSessionService, MemberSessionService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ITrainerService, TrainerService>();
builder.Services.AddIdentity<AppUser, IdentityRole>(config =>
{
    config.User.RequireUniqueEmail = true;
    config.Lockout.MaxFailedAccessAttempts = 5;
    config.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
})
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(opt =>
{
    opt.LoginPath = "/Account/Login";
    opt.AccessDeniedPath = "/Account/AccessDenied";
});

builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();
builder.Services.AddScoped<ISessionService,SessionService>();
builder.Services.AddScoped<FilesFactory>();
builder.Services.AddScoped<ImageUploader>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();
    var Role = scope.ServiceProvider.GetRequiredService <RoleManager<IdentityRole>>();
    var User = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

    var PendingMigrations = await context.Database.GetPendingMigrationsAsync();
    if (PendingMigrations?.Any() ?? false) await context.Database.MigrateAsync();

    await GymDbContextDataSeeding.SeedData(context, env);
    await IdentityDbContextDataSeeding.SeedData(Role, User);
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseMiddleware<SoftDeleteMiddleWare>();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
