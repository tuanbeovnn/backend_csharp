using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Email;

namespace Business.Extensions;

public static class LoggingExtensions
{
    public static ConfigureHostBuilder AddSerilog(this ConfigureHostBuilder builder)
    {
        builder.UseSerilog((hostingContext, services, loggerConfiguration) =>
        {
            loggerConfiguration.WriteTo.Console();
            loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration);
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File("logs/all-.txt", rollingInterval: RollingInterval.Day)
                .WriteTo.File("logs/errors-.txt", restrictedToMinimumLevel: LogEventLevel.Error,
                    rollingInterval: RollingInterval.Day)
                // .WriteTo.Email(new EmailSinkOptions
                // {
                //     From = "sender@example.com",
                //     To = new List<string> { "recipient@example.com" },
                //     Host = "smtp.gmail.com",
                //     Port = 587,
                //     Credentials = new NetworkCredential("username", "password"),
                // }, restrictedToMinimumLevel: LogEventLevel.Fatal)
                .CreateLogger();
        }).ConfigureLogging(f =>
        {
            f.ClearProviders();
            f.AddSerilog();
        });
        return builder;
    }
}