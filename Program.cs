using Application.Services.DIContainer;
using Serilog;
using System.Text.Json.Serialization;
using Asp.Versioning;
using Asp.Versioning.Conventions;
using FluentValidation;
using NewSampleAPI.Validation;
using NewSampleAPI.Domain.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Json Enum Conversion
builder.Services.AddControllers()
    .AddJsonOptions(o => o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

//Loging
builder.Host.UseSerilog((c, loggerConfig) =>
{
    loggerConfig.ReadFrom.Configuration(c.Configuration);
},false,true);

builder.Services.AddSingleton(Log.Logger);

builder.Logging.AddRinLogger();

builder.Services.AddRin();

//Validator
builder.Services.AddScoped<IValidator<CalcModel>, CalcValidator>();

builder.Services.AddAppServices(builder.Configuration);

//Api Version
builder.Services.AddApiVersioning(v =>
{
    v.DefaultApiVersion = new ApiVersion(1.0);
    v.AssumeDefaultVersionWhenUnspecified = true;
    v.ReportApiVersions = true;

}).AddMvc(o =>
{
    o.Conventions.Add(new VersionByNamespaceConvention());
}
);

//Adding cors
builder.Services.AddCors(option =>
{
    option.AddDefaultPolicy(
            policy => {
                policy.WithOrigins("https://localhost:7108", "http://localhost:5154")
                .AllowAnyMethod()
                .AllowAnyHeader();
            }
        );
});

builder.Services.AddValidatorsFromAssemblyContaining<CalcValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseRin();
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseRinDiagnosticsHandler();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

