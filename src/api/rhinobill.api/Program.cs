using Newtonsoft.Json;
using rhinobill.api.Middlewares;
using rhinobill.core;
using rhinobill.core.Converters;
using rhinobill.sql;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        var setting = JsonSerializeSetting.GetSettings();
        foreach (var converter in setting.Converters)
        {
            options.SerializerSettings.Converters.Add(converter);
        }
    });

JsonConvert.DefaultSettings = JsonSerializeSetting.GetSettings;

builder.Services
    .AddSql(configuration)
    .AddCore();

builder.Services.AddTransient<ErrorHandlerMiddleware>();

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
app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();


public partial class Program;