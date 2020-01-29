using ControleApData.Client;
using SimpleInjector;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ControleApData
{
    class Program
    {
        static Task Main(string[] args)
        {
            var container = new Container();

            container.Register<Worker>();
            container.RegisterSingleton(() => new CookieContainer()); container.Register(() =>
            {
                var handler = new HttpClientHandler();
                handler.CookieContainer = container.GetInstance<CookieContainer>();
                return handler;
            });
            container.Register(() => new HttpClient(container.GetInstance<HttpClientHandler>()));
            container.Register<ApDataLowLevelClient>();
            container.Register<ApDataClient>();
            container.Register<GetApDataBaseUrl>(() => () => "https://cliente.apdata.com.br/braspag/.net/index.ashx");

            var worker = container.GetInstance<Worker>();
            return worker.Work();
        }
    }
}
