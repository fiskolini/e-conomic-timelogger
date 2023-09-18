using System;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using TimeLogger.Application.Common.Exceptions;

namespace TimeLogger.Api.Extensions
{
    public static class ErrorHandlerExtensions
    {
        public static void UseErrorHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature == null) return;

                    context.Response.ContentType = "application/json";

                    switch (contextFeature.Error)
                    {
                        case BadRequestException _:
                            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                            break;
                        case OperationCanceledException _:
                            context.Response.StatusCode = (int)HttpStatusCode.ServiceUnavailable;
                            break;
                        case NotFoundException _:
                            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                            break;
                        default:
                            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                            break;
                    }

                    var errorResponse = new
                    {
                        statusCode = context.Response.StatusCode,
                        message = contextFeature.Error.GetBaseException().Message
                    };

                    await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
                });
            });
        }
    }
}