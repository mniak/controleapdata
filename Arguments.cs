using System;
using CommandLine;

namespace ApdataTimecardFixer
{
    public class Arguments
    {
        [Option(SetName = "url", Required=true, HelpText="APDATA base url")]
        public string BaseUrl { get; set; }

        [Option(SetName = "slug", Required=true, HelpText="The company name on the apdata platform. Uses the default url format: https://cliente.apdata.com.br/COMPANY")]
        public string Company { get; set; }

        [Option('U', "username", Required=true, HelpText = "Your username")]
        public string Username { get; set; }

        [Option('P', "password", Required=true, HelpText = "Your password")]
        public string Password { get; set; }

        [Value(0, Required=true, HelpText="The year")]
        public int Year { get; set; }

        [Value(1, Required=true, HelpText="The month")]
        public int Month { get; set; }
    }
}