using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Sample.WebApi;
using Sample.WebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// BEGIN telemetry
builder.Services.AddLogging(l =>
{
    l.AddOpenTelemetry(x => {
        x.SetResourceBuilder(
            ResourceBuilder
                .CreateDefault()
                .AddService(DiagnosticsConfig.Meter.Name));
        x.AddOtlpExporter();
        
    });
});

builder.Services.AddOpenTelemetry()
    .ConfigureResource(rb => rb.AddService(DiagnosticsConfig.Meter.Name))
    .WithMetrics(meterProviderBuilder =>
    {
        meterProviderBuilder
            .AddMeter(DiagnosticsConfig.Meter.Name)
            .AddOtlpExporter();
    })
    .WithTracing(tracerProviderBuilder =>
    {
        tracerProviderBuilder
            .AddAspNetCoreInstrumentation()
            .AddSqlClientInstrumentation()
            .AddHttpClientInstrumentation()
            .AddOtlpExporter();
    });

builder.Services
    .AddSingleton<TenantIdProcessor>()
    .ConfigureOpenTelemetryTracerProvider((serviceProvider, tracerProviderBuilder) =>
    {
        tracerProviderBuilder.AddProcessor(
            serviceProvider.GetRequiredService<TenantIdProcessor>());
    });
// END telemetry

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
