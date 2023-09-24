using System;
using System.Collections.Generic;
using System.Dynamic;
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
                    string[] errors = null;
                    if (contextFeature == null) return;

                    context.Response.ContentType = "application/json";

                    switch (contextFeature.Error)
                    {
                        case BadRequestException exception:
                            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                            errors = exception.Errors;
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

                    var errorResponse = new ExpandoObject() as IDictionary<string, object>;

                    errorResponse["statusCode"] = context.Response.StatusCode;
                    errorResponse["message"] = contextFeature.Error.GetBaseException().Message;

                    if (errors != null)
                    {
                        errorResponse["errors"] = errors;
                    }

                    await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
                });
            });
        }
    }
}