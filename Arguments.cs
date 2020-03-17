using System;
using CommandLine;

namespace ControleApData
{
    public class Arguments
    {
        [Option(SetName = "url", Required=true)]
        public string BaseUrl { get; set; }

        [Option(SetName = "slug", Required=true)]
        public string Company { get; set; }

        [Option('U', "username", Required=true, HelpText = "Your username")]
        public string Username { get; set; }

        [Option('P', "password", Required=true, HelpText = "Your password")]
        public string Password { get; set; }

        [Option("year")]
        public int Year { get; set; } = DateTime.Now.Year;

        [Option("month")]
        public int Month { get; set; } = DateTime.Now.Month;
    }
}