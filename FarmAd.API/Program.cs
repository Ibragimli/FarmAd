using FarmAd.Persistence.Contexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Collections.ObjectModel;
using System.Text;
using FarmAd.Persistence;
using FarmAd.Infrastructure;
using FarmAd.Application;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using FluentValidation.AspNetCore;
using FarmAd.Application.DTOs.User;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpContextAccessor();
builder.Services.AddPersistenceServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices();
//builder.Services.AddSignalRServices();

//builder.Services.AddStorage<LocalStorage>();
//builder.Services.AddStorage<AzureStorage>();
//builder.Services.AddStorage(ETicaretApi.Infrastructure.Enums.StorageType.Local);


builder.Services.AddControllers().AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<ProductCreateDto>());



//builder.Services.AddCors(opt => opt.AddDefaultPolicy(policy =>
//    policy.WithOrigins("https://localhost:7151").WithOrigins("http://localhost:7151").AllowAnyHeader().AllowAnyMethod().AllowCredentials()));

//builder.Services.AddControllers(options =>
//{
//    options.Filters.Add<ValidationFilter>();
//    options.Filters.Add<RolePermissionFilter>();
//})
//    .AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>());
//.ConfigureApiBehaviorOptions(opt => opt.SuppressModelStateInvalidFilter = true);
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();


#region Log


//SqlColumn sqlColumn = new SqlColumn();
//sqlColumn.ColumnName = "Username";
//sqlColumn.DataType = System.Data.SqlDbType.NVarChar;
//sqlColumn.PropertyName = "Username";
//sqlColumn.DataLength = 50;
//sqlColumn.AllowNull = true;
//ColumnOptions columnOpts = new ColumnOptions();
//columnOpts.Store.Remove(StandardColumn.Properties);
//columnOpts.Store.Add(StandardColumn.LogEvent);
//columnOpts.AdditionalColumns = new Collection<SqlColumn> { sqlColumn };

//// Logger log = new LoggerConfiguration().CreateLogger();
//Logger log = new LoggerConfiguration()
//    // ConsoleLog
//    .WriteTo.Console()
//    // Filelog
//    .WriteTo.File("logs/log.txt")
//    // DB log
//    .WriteTo.MSSqlServer(connectionString: builder.Configuration.GetConnectionString("Default"),

//          sinkOptions: new MSSqlServerSinkOptions
//          {
//              AutoCreateSqlTable = true,
//              TableName = "LogEvents",
//          }, appConfiguration: null, columnOptions: columnOpts)
//    .WriteTo.Seq(builder.Configuration["Seq:ServerURL"])
//    .Enrich.FromLogContext()
//    .Enrich.With<CustomUserNameColumnWriter>()
//    .MinimumLevel.Information()
//    .CreateLogger();

//builder.Host.UseSerilog(log);
#endregion 

#region htttpLog

//htttpLog
//builder.Services.AddHttpLogging(logging =>
//{
//    logging.LoggingFields = HttpLoggingFields.All;
//    logging.RequestHeaders.Add("sec-ch-ua");
//    logging.MediaTypeOptions.AddText("application/javascript");
//    logging.RequestBodyLogLimit = 4096;
//    logging.ResponseBodyLogLimit = 4096;
//});
//htttpLog
#endregion 

#region JwtBearer
//JwtBearer
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//        .AddJwtBearer("Admin", opt =>
//        opt.TokenValidationParameters = new()
//        {
//            ValidateAudience = true,
//            ValidateIssuer = true,
//            ValidateLifetime = true,
//            ValidateIssuerSigningKey = true,
//            ValidAudience = builder.Configuration["Token:Audience"],
//            ValidIssuer = builder.Configuration["Token:Issuer"],
//            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
//            LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null && expires > DateTime.UtcNow.AddSeconds(-1),
//        });
//JwtBearer
#endregion 

builder.Services.AddDbContext<DataContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

// Add services to the container.

builder.Services.AddControllers();
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
