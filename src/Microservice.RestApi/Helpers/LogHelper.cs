using Microsoft.AspNetCore.Http;
using Serilog;
using Serilog.Events;
using System;

namespace LetItGrow.Microservice.RestApi.Helpers
{
    // Read for more info at:
    // https://andrewlock.net/using-serilog-aspnetcore-in-asp-net-core-3-excluding-health-check-endpoints-from-serilog-request-logging/

    public static class LogHelper
    {
        public static void EnrichFromRequest(IDiagnosticContext diagnosticContext, HttpContext httpContext)
        {
            var request = httpContext.Request;

            // Set all the common properties available for every request
            diagnosticContext.Set("Host", request.Host);
            diagnosticContext.Set("Protocol", request.Protocol);
            diagnosticContext.Set("Scheme", request.Scheme);

            // Only set it if available. You're not sending sensitive data in a querystring right?!
            if (request.QueryString.HasValue)
            {
                diagnosticContext.Set("QueryString", request.QueryString.Value);
            }

            // Set the content-type of the Response at this point
            diagnosticContext.Set("ContentType", httpContext.Response.ContentType);

            // Retrieve the IEndpointFeature selected for the request
            var endpoint = httpContext.GetEndpoint();
            if (endpoint is not null) // endpoint != null
            {
                diagnosticContext.Set("EndpointName", endpoint.DisplayName);
            }
        }

        public static LogEventLevel CustomGetLevel(HttpContext ctx, double _, Exception ex) =>
            ex != null
                ? LogEventLevel.Error 
                : ctx.Response.StatusCode > 499 
                    ? LogEventLevel.Error 
                    : LogEventLevel.Debug; //Debug instead of Information
    }
}