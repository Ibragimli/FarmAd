using Microsoft.AspNetCore.Diagnostics;
using System.Net.Mime;
using System.Net;
using System.Text.Json;

namespace FarmAd.API.Extensions
{
    static public class ConfigureExceptionHandlerExtension
    {
        public static void ConfigureExceptionHandler<T>(this WebApplication application, ILogger<T> logger)
        {
            application.UseExceptionHandler(builder =>
            {
                builder.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = MediaTypeNames.Application.Json;

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        var exception = contextFeature.Error;
                        logger.LogError(exception, "An error occurred: {Message}", exception.Message);

                        var response = new
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = exception.Message,
                            Title = "Error received!",
                            ExceptionType = exception.GetType().ToString()  // Optional, adds exception type info
                        };

                        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                    }
                });

            });
        }
    }
}
