using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Api.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private static IDictionary<Type, HttpStatusCode> Mappings => new Dictionary<Type, HttpStatusCode>
        {
            {typeof (ArgumentNullException), HttpStatusCode.BadRequest},
            {typeof (ArgumentException), HttpStatusCode.BadRequest},
            {typeof (ArgumentOutOfRangeException), HttpStatusCode.BadRequest},
            {typeof (InvalidOperationException), HttpStatusCode.BadRequest},
            {typeof (JsonReaderException), HttpStatusCode.BadRequest },
            {typeof (KeyNotFoundException), HttpStatusCode.BadRequest },
            {typeof (WebException), HttpStatusCode.BadRequest }
        };

        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (HttpException httpException)
            {
                context.Response.StatusCode = httpException.StatusCode;
                var responseFeature = context.Features.Get<IHttpResponseFeature>();
                responseFeature.ReasonPhrase = httpException.Message;
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var ex = exception?.InnerException;

            if (exception == null) 
            { 
                return; 
            }

            var code = Mappings.ContainsKey(exception.GetType()) ? Mappings[exception.GetType()] : HttpStatusCode.InternalServerError;
            await WriteExceptionAsync(context, ex ?? exception, code).ConfigureAwait(false);
        }

        private static async Task WriteExceptionAsync(HttpContext context, Exception exception, HttpStatusCode code)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)code;
            await response.WriteAsync(JsonConvert.SerializeObject(new
            {
                error = new
                {
                    message = exception.Message,
                    exception = exception.GetType().Name
                }
            })).ConfigureAwait(false);
        }
    }
}
