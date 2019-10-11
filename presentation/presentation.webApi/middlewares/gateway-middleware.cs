using domain.application;
using domain.repository.collections;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Newtonsoft.Json;
using Serilog;
using shared.utility._app;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace presentation.webApi.middlewares {
    public static class GatewayExtensions {
        public static IApplicationBuilder UseGateway(this IApplicationBuilder builder) {
            return builder.UseMiddleware<GatewayMiddleware>();
        }
    }
    public class GatewayMiddleware {
        #region Constructor
        private readonly IHttpLog_Service _httpLogService;
        private readonly IException_Service _exceptionService;
        private readonly RequestDelegate _requestDelegate;
        public GatewayMiddleware(IHttpLog_Service httpLogService, IException_Service exceptionService, RequestDelegate requestDelegate) {
            _httpLogService = httpLogService;
            _exceptionService = exceptionService;
            _requestDelegate = requestDelegate;
        }
        #endregion

        #region Private
        private async Task<string> FormatRequest(HttpRequest request) {
            var body = request.Body;
            request.EnableRewind();
            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            await request.Body.ReadAsync(buffer, 0, buffer.Length);
            var bodyAsText = Encoding.UTF8.GetString(buffer);
            request.Body = body;
            return bodyAsText;
        }
        private async Task<string> FormatResponse(HttpResponse response) {
            response.Body.Seek(0, SeekOrigin.Begin);
            var text = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);
            return text;
        }
        #endregion

        public async Task InvokeAsync(HttpContext context) {
            var ip = context.Connection.RemoteIpAddress.ToString();
            var url = $"{context.Request.Scheme}://{context.Request.Host}{context.Request.Path}{context.Request.QueryString}";
            var requestedAt = DateTime.Now;
            var stopWatch = new Stopwatch();
            var response = string.Empty;
            stopWatch.Start();
            var originalBodyStream = context.Response.Body;
            using(var responseBody = new MemoryStream()) {
                context.Response.Body = responseBody;
                await _requestDelegate(context);
                response = await FormatResponse(context.Response);
                await responseBody.CopyToAsync(originalBodyStream);
            }
            stopWatch.Stop();
            var model = new HttpLog {
                IP = ip,
                URL = url,
                Method = context.Request.Method,
                RequsetHeader = (from t in context.Request.Headers select t).ToDictionary(x => x.Key, x => x.Value.ToArray()),
                ReqestedAt = requestedAt,
                Request = (await FormatRequest(context.Request)).Replace("\n\t", string.Empty),
                ResponseHeader = (from t in context.Response.Headers select t).ToDictionary(x => x.Key, x => x.Value.ToArray()),
                ResponsedAt = DateTime.Now,
                Response = response,
                Duration = stopWatch.ElapsedMilliseconds
            };
            //try {
            if (AppSettings.MongoLogging) {
                await _httpLogService.InsertAsync(model, 5000);
            }
            if (AppSettings.FileLogging) {
                Log.Debug(JsonConvert.SerializeObject(model));
            }
            if(AppSettings.SqlLogging) {

            }
            //}
            //catch (Exception ex) {
            //    await _exceptionService.InsertAsync(ex, url, ip);
            //}
            await _requestDelegate(context);
        }
    }
}
