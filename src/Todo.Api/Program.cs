using Microsoft.Extensions.Configuration;
using Todo.Api.models;
using Todo.Api.services.weather.repository.mysql;
using Todo.Api.services.weather.repository.nosql;
using Todo.Api.services.weather.usecase;

static void initOthers(WebApplicationBuilder builder)
{
}
static void initRepositories(WebApplicationBuilder builder)
{
    string connStr = builder.Configuration["Databases:mysql"];
    builder.Services.AddScoped<IWeatherForecastMysqlRepo, mysqlWeatherForecast>(x => new mysqlWeatherForecast(connStr));
    builder.Services.AddScoped<IWeatherForecastNoSqlRepo, nosqlWeatherForecast>();

}
static void initUsecase(WebApplicationBuilder builder)
{
    builder.Services.AddScoped<IWeatherForecastUsecase, weatherUsecase>();
}


var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers();
initOthers(builder);
initRepositories(builder);
initUsecase(builder);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
