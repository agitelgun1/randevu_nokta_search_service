using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using RandevuNokta.Search.Api.Models;
using RandevuNokta.Search.Api.Services;
using UAParser;

namespace RandevuNokta.Search.Api.Middleware
{
    public class ErrorMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IErrorService _errorLog;

        public ErrorMiddleware(RequestDelegate next, IErrorService errorLog)
        {
            _next = next;
            _errorLog = errorLog;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                //Device,os
                var userAgent = context.Request.Headers["User-Agent"];
                var uaString = Convert.ToString(userAgent[0]);
                var uaParser = Parser.GetDefault();
                var client = uaParser.Parse(uaString);

                //Ip
                var remoteIpAddress = context.Connection.RemoteIpAddress.MapToIPv4().ToString();
                if (context.Request.Headers.ContainsKey("X-Forwarded-For"))
                    remoteIpAddress = context.Request.Headers["X-Forwarded-For"];
                
                

                var error = new ErrorLog
                {
                    ErrorMessage = ex.Message,
                    ErrorStacktrace = ex.StackTrace,
                    ProjectName = "SearchService",
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Device = client.UA.Family,
                    DeviceVersion = client.UA.Major + "." + client.UA.Minor + "." + client.UA.Patch,
                    OperatingSystem = client.OS.ToString(),
                    Ip = remoteIpAddress,
                    Path = context.Request.Path
                };

                await _errorLog.InsertErrorLog(error);
            }
        }
    }
}