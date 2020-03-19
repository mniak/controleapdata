using CommandLine;
using ApdataTimecardFixer.Client;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace ApdataTimecardFixer
{
    public class Program
    {
        static Task<int> Main(string[] args)
        {
            return CommandLine.Parser.Default.ParseArguments<Arguments>(args)
                .MapResult(
                    async x => await Run(x, log => {
                        log.AddConsole();
                    }), 
                          errs => Task.FromResult(1));
        }

        public static async Task<int> Run(Arguments arguments, Action<ILoggingBuilder> logBuilder)
        {
            var worker = new ServiceCollection()
                .AddScoped<Worker>()
                .AddSingleton(svc=> new CookieContainer())
                .AddScoped(svc =>
                {
                    var handler = new HttpClientHandler();
                    handler.CookieContainer = svc.GetRequiredService<CookieContainer>();
                    return handler;
                })
                .AddLogging(logBuilder)
                .AddScoped(svc => arguments)
                .AddScoped(svc => new HttpClient(svc.GetRequiredService<HttpClientHandler>()))
                .AddScoped<ApdataLowLevelClient>()
                .AddScoped<ApdataClient>()
                .BuildServiceProvider()
                .GetRequiredService<Worker>();
            await worker.Work();
            return 0;
        }
    }
}
