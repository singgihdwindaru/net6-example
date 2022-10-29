using Todo.Api.models;
using Todo.Api.Repositories.NoSql;


static void initOthers (WebApplicationBuilder builder)
{
    builder.Services.AddScoped<IWeatherForecastNoSqlRepo, weatherForcastNoSqlRepo>();
}
static void initRepositories (WebApplicationBuilder builder)
{
    builder.Services.AddScoped<IWeatherForecastNoSqlRepo, weatherForcastNoSqlRepo>();
}
static void initServices (WebApplicationBuilder builder)
{
    builder.Services.AddScoped<IWeatherForecastNoSqlRepo, weatherForcastNoSqlRepo>();
}

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
initOthers(builder);
initRepositories(builder);
initServices(builder);

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
