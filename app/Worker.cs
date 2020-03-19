using ApdataTimecardFixer.Client;
using ApdataTimecardFixer.Client.Models;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace ApdataTimecardFixer
{
    public class Worker
    {
        private readonly Client.ApdataClient apDataClient;
        private readonly Arguments args;
        private readonly ILogger<Worker> log;

        public Worker(Client.ApdataClient apDataClient, Arguments args, ILogger<Worker> log)
        {
            this.apDataClient = apDataClient ?? throw new ArgumentNullException(nameof(apDataClient));
            this.args = args ?? throw new ArgumentNullException(nameof(args));
            this.log = log ?? throw new ArgumentNullException(nameof(log));
        }

        public async Task Work()
        {
            log.LogInformation("-> Login");
            var loginResponse = await apDataClient.Login(args.Username, args.Password);

            log.LogInformation("-> Load screen");
            var gridMetadata = await apDataClient.CreateEditGridAndGetHeaders("ScreenConBatidasReaisClassifsProvider");

            log.LogInformation("-> Selecting period {Month}/{Year}", args.Month, args.Year);
            var setProviderStatus = await apDataClient.SetProviderParams(gridMetadata.Hwd, args.Year, args.Month, loginResponse.SelectedEmployee);

            log.LogInformation("-> Download grid");
            var gridData = await apDataClient.GetEditGridPage(gridMetadata.Hwd);

            var emptyWorkdays = gridData.Recs
                .Where(x => x.Tipo == TipoDeDia.Normal)
                .Where(x => x.Status == StatusDoDia.Normal)
                .Where(x => string.IsNullOrEmpty(x.Entrada1) || string.IsNullOrEmpty(x.Saida1));

            log.LogInformation("-> Fill empty boxes");
            foreach (var rec in emptyWorkdays)
            {
                var parsedShift = Regex.Match(rec.Field73, @"^\d+ - (\d+:\d+) (\d+:\d+) (\d+:\d+) (\d+:\d+)\b");
                var (start, end) = parsedShift.Success
                    ? (parsedShift.Groups[1].Value, parsedShift.Groups[4].Value)
                    : ("09:00", "18:30");

                var parsedDate = Regex.Match(rec.Field1, @"^(\d+)/(\d+)\b");
                var realDate = parsedDate.Success
                    ? new DateTime(args.Year, args.Month, int.Parse(parsedDate.Groups[1].Value))
                    : new DateTime(args.Year, args.Month, rec.Field72.Day);

                log.LogInformation($"  -> Updating {realDate} to {start}-{end}. ");
                var result = await apDataClient.UpdateProviderRecord(gridMetadata.Hwd, rec.Field1, realDate, rec.Status, start, end);
                log.LogInformation($"Success={result.Success}");
            }
        }
    }
}
