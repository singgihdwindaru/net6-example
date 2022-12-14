using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Todo.Api.models;
using Todo.Api.services.weather.repository.mysql;
using Todo.Api.services.weather.repository.nosql;
using Todo.Api.services.weather.usecase;
using static Todo.Api.models.weatherForecastModel;

var builder = WebApplication.CreateBuilder(args);
string connStr = builder.Configuration["Databases:mysql"];

void initOthers(WebApplicationBuilder builder)
{
}
void initRepositories(WebApplicationBuilder builder)
{
    var mysqlConn = new MySqlConnection(connStr);

    // builder.Services.AddScoped<IWeatherForecastMysqlRepo, mysqlWeatherForecast>(x => new mysqlWeatherForecast(connStr));
    builder.Services.AddScoped<IWeatherForecastMysqlRepo, mysqlWeatherForecast>(x =>
    {
        var repo = new mysqlWeatherForecast(mysqlConn);
        return repo;
    });
    builder.Services.AddScoped<IWeatherForecastNoSqlRepo, nosqlWeatherForecast>();
}
void initUsecase(WebApplicationBuilder builder)
{
    builder.Services.AddScoped<IWeatherForecastUsecase, weatherUsecase>();
}

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
