using Coravel;
using CREL_BE.Extensions;
using CREL_BE.Invocables;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices(builder.Configuration);

FirebaseExtension.Init();

builder.Services.AddIdentityServices(builder.Configuration);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerConfig(builder.Configuration);
// Add Automapper to the container.
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//add Coravel
builder.Services.AddScheduler();
builder.Services.AddTransient<HourlyCornJob>();
builder.Services.AddTransient<DailyCronJob>();
builder.Services.AddTransient<DailyAt7amCornJob>();
builder.Services.AddTransient<DailyAt7pmCornJob>();

var app = builder.Build();

// // Configure the HTTP request pipeline.
// //if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();

app.UseCors(x => x.AllowAnyHeader()
                  .AllowAnyMethod()
                  //.AllowCredentials()
                  //.AllowAnyOrigin())
                  .SetIsOriginAllowed(host => true)
);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

//Auto update database
// await app.UpdateDatabaseAsync();

//use Coravel
var provider = app.Services;
provider.UseScheduler(scheduler =>
{
    scheduler.Schedule<HourlyCornJob>().Hourly();
    scheduler.Schedule<DailyCronJob>().Daily();
    scheduler.Schedule<DailyAt7amCornJob>().DailyAtHour(7).Zoned(TimeZoneInfo.Local);
    scheduler.Schedule<DailyAt7pmCornJob>().DailyAtHour(19).Zoned(TimeZoneInfo.Local);
});

//Word export
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mgo+DSMBMAY9C3t2VVhjQlFac1ZJXGFWfVJpTGpQdk5xdV9DaVZUTWY/P1ZhSXxRd0djX39Xc3JXRGJfU0Q=");

app.Run();
