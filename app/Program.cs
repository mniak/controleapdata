using CommandLine;
using ApdataTimecardFixer.Client;
using PowerArgs;
using SimpleInjector;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApdataTimecardFixer
{
    class Program
    {
        static Task<int> Main(string[] args)
        {
            return CommandLine.Parser.Default.ParseArguments<Arguments>(args)
                .MapResult(
                    async x => await RunWithArguments(x), 
                          errs => Task.FromResult(1));
        }

        public static async Task<int> RunWithArguments(Arguments arguments)
        {
            var container = new Container();
            container.Register<Worker>();
            container.RegisterSingleton(() => new CookieContainer()); container.Register(() =>
            {
                var handler = new HttpClientHandler();
                handler.CookieContainer = container.GetInstance<CookieContainer>();
                return handler;
            });
            container.Register(() => arguments);
            container.Register(() => new HttpClient(container.GetInstance<HttpClientHandler>()));
            container.Register<ApdataLowLevelClient>();
            container.Register<ApdataClient>();

            var worker = container.GetInstance<Worker>();
            await worker.Work();
            return 0;
        }
    }
}
