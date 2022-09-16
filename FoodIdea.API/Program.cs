using FoodIdea.API;
using FoodIdea.API.DbContexts;
using FoodIdea.API.Services;
using Microsoft.AspNetCore.StaticFiles;
using Serilog;
using Microsoft.EntityFrameworkCore;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("logs/foodidea.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();

// Allows to use XML
builder.Services.AddControllers(options =>
{
    options.ReturnHttpNotAcceptable = true;
}).AddNewtonsoftJson()
.AddXmlDataContractSerializerFormatters();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<FileExtensionContentTypeProvider>();

builder.Services.AddTransient<IMailService,LocalMailService>();

builder.Services.AddSingleton<FoodDataStore>();
//Not a very secure solution, I should use Environment Variables or Azure 
builder.Services.AddDbContext<FoodIdeaContext>(
            dbContextOptions => dbContextOptions
                .UseMySql(builder.Configuration["ConnectionStrings:FoodIdeaDBConnectionString"],
                ServerVersion.AutoDetect(builder.Configuration["ConnectionStrings:FoodIdeaDBConnectionString"])));

builder.Services.AddScoped<IFoodIdeaRepository, FoodIdeaRepository>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
