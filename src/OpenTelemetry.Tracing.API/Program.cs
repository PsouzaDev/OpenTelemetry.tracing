using OpenTelemetry.Tracing.API.Class;
using OpenTelemetry.Trace;
using OpenTelemetry.Resources;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddSingleton<IUserProvider>(new UserProvider("Server=(LocalDB)\\MSSQLLocalDB;Integrated Security=true;AttachDbFileName=C:\\Users\\pedro\\source\\repos\\OpenTelemetry.tracing\\OTM_DB.mdf"));

var serviceName = "StreetService";

builder.Services.AddOpenTelemetryTracing(b => {
    b.AddConsoleExporter()
    .AddSource(serviceName)
    .SetResourceBuilder(
        ResourceBuilder.CreateDefault().AddService(serviceName, serviceVersion: "1.0.0")
    )
    .AddAspNetCoreInstrumentation()
    .AddSqlClientInstrumentation()
    .AddHttpClientInstrumentation();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
