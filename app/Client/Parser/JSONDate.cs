using ApdataTimecardFixer.Client.Parser.SyntaxTree;
using System;

namespace ApdataTimecardFixer.Client.Parser
{
    internal class JSONDate : IJSONValue
    {
        private readonly int year;
        private readonly int month;
        private readonly int day;

        public JSONDate(string yearStr, string monthStr, string dayStr)
        {
            year = int.Parse(yearStr);
            month = int.Parse(monthStr) + 1;
            day = int.Parse(dayStr);
        }

        public IJSONValue this[string key] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IJSONValue this[int i] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string ToJSON()
        {
            var result = $"\"{year.ToString("0000")}-{month.ToString("00")}-{day.ToString("00")}T00:00:00Z\"";
            return result;
        }
    }
}