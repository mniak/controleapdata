using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ApdataTimecardFixer;
using System.Text;

namespace Mniak.Automation
{
    public static class FixTimecard
    {
        [FunctionName("FixTimecard")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = null)] HttpRequest req)
        {
            var logger = new MemoryLogger();
            logger.LogInformation("C# HTTP trigger function processed a request.");

            using (var streamReader = new StreamReader(req.Body))
            {
                var requestBody = await streamReader.ReadToEndAsync();
                var arguments = JsonConvert.DeserializeObject<Arguments>(requestBody);

                try
                {
                    await Program.Run(arguments, logger);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error while fixing timecard");
                }

                return new OkObjectResult(new {
                    Messages = logger.Messages
                });
            }
        }
    }
}
