using Microsoft.EntityFrameworkCore;
using Npgsql;
using Payment.Api;
using Payment.Bussinies.Repositories;
using Payment.Bussinies.Repositories.Interfaces;
using Payment.Core.Repositories;
using Payment.Core.Repositories.Base;
using Payment.Data.Context;
using Payment.Data.Repositories;
using Payment.Providers.Cache;
using Payment.Providers.Cache.Memory;
using Payment.Providers.Cache.Models;
using Payment.Providers.Serilizer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSwaggerConfiguration();
builder.Services.AddApiVersioningExtension();
builder.Services.AddJwtTokenAuthentication(builder.Configuration);
var connectionString = builder.Configuration["PostgreSql:ConnectionString"];
var dbPassword = builder.Configuration["PostgreSql:DbPassword"];
var npSql = new NpgsqlConnectionStringBuilder(connectionString)
{
    Password = dbPassword
};
builder.Services.AddDbContext<PaymentContext>(options => options.UseNpgsql(npSql.ConnectionString, options => options.MigrationsAssembly("Payment.Data")));

builder.Services.AddScoped<IPaymentAvtivityRepository, PaymentActivityRepository>();
builder.Services.AddScoped<IPaymentBinNumberRepository, PaymentBinNumberRepository>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();

///Initilize From Config
var serilizer = builder.Configuration["Serilizer"];
var redisConfig = builder.Configuration["Redis:RedisConnectionString"];
CacherConfiguration.Initilize(serilizer, redisConfig);

var app = builder.Build();
app.UseSwaggerSetup();

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();
app.UseAuthentication();
app.MigrateDatabase();
app.Run();
