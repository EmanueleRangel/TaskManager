using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Globalization;
using System.Net;

namespace TaskManager.API.Health
{
    public class HealthChecker
    {
        public const string Path = "/health";

        private static readonly JsonSerializerSettings JSON_SETTINGS = new JsonSerializerSettings()
        {
            Converters = new List<JsonConverter>() { new StringEnumConverter() },
            Culture = CultureInfo.InvariantCulture,
            Formatting = Formatting.Indented,
            NullValueHandling = NullValueHandling.Ignore
        };

        public static async Task WriteHealthResponse(HttpContext httpContext, HealthReport healthReport)
        {
            var serviceProvider = httpContext.RequestServices;
            var webHostEnvironment = serviceProvider.GetRequiredService<IWebHostEnvironment>();

            var response = new
            {
                Name = webHostEnvironment.ApplicationName,
                Host = Environment.MachineName,
                Environment = webHostEnvironment.EnvironmentName,
                healthReport.Status,
                healthReport.TotalDuration,
                Dependencies = healthReport.Entries.Select(ToDependencyObject)
            };

            httpContext.Response.StatusCode = (int)HttpStatusCode.OK;
            httpContext.Response.ContentType = "application/json";

            var serializedResponse = JsonConvert.SerializeObject(response, JSON_SETTINGS);
            await httpContext.Response.WriteAsync(serializedResponse);
        }

        private static object ToDependencyObject(KeyValuePair<string, HealthReportEntry> healthReportEntryDict)
        {
            var healthReportEntry = healthReportEntryDict.Value;
            return new
            {
                Name = healthReportEntryDict.Key,
                healthReportEntry.Status,
                healthReportEntry.Description,
                healthReportEntry.Duration,
                AdditionalData = (healthReportEntry.Data.Count > 0) ? healthReportEntry.Data : default,
            };
        }
    }
}
