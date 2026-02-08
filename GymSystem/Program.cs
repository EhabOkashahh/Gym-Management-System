using GymSystem.Extension.Classes;
using GymSystemBLL.Services.Classes;
using GymSystemBLL.Services.Interfaces;
using GymSystemDAL.Data.Contexts;
using GymSystemDAL.Data.Seeding;
using GymSystemDAL.Repositories.Classes;
using GymSystemDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IUnitOfWork , UnitOfWork>();
builder.Services.AddScoped<IMemberService , MemberService>();
builder.Services.AddScoped<IPlanService,PlanService>();
builder.Services.AddScoped<IMemberShipService,MemberShipService>();
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

    await GymDbContextDataSeeding.SeedData(context, env);
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
