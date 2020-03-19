using ApdataTimecardFixer.Client.Parser.SyntaxTree;
using Sprache;
using System;
using System.Collections.Generic;

namespace ApdataTimecardFixer.Client.Parser
{
    /// <summary>
    /// Contains the entire JSON Parser
    /// </summary>
    static class JSONParser
    {
        /// <summary>
        /// Parses a literal null value
        /// </summary>
        static readonly Parser<JSONLiteral> JNull =
            from str in Parse.IgnoreCase("null")
            select new JSONLiteral(null, LiteralType.Null);

        /// <summary>
        /// Parses a literal boolean value
        /// </summary>
        static readonly Parser<JSONLiteral> JBoolean =
            from str in Parse.IgnoreCase("true").Text().Or(Parse.IgnoreCase("false").Text())
            select new JSONLiteral(str, LiteralType.Boolean);

        /// <summary>
        /// Parses the exponential part of a number
        /// </summary>
        static readonly Parser<string> JExp =
            from e in Parse.IgnoreCase("e").Text()
            from sign in Parse.String("+").Text()
                         .Or(Parse.String("-").Text())
                         .Optional()
            from digits in Parse.Digit.Many().Text()
            select e + ((sign.IsDefined) ? sign.Get() : "") + digits;

        /// <summary>
        /// Parses the decimal part of a number
        /// </summary>
        static readonly Parser<string> JFrac =
            from dot in Parse.String(".").Text()
            from digits in Parse.Digit.Many().Text()
            select dot + digits;

        /// <summary>
        /// Parses the integer part of anumber
        /// </summary>
        static readonly Parser<string> JInt =
            from minus in Parse.String("-").Text().Optional()
            from digits in Parse.Digit.Many().Text()
            select (minus.IsDefined ? minus.Get() : "") + digits;

        /// <summary>
        /// Parses a JSON number
        /// </summary>
        static readonly Parser<JSONLiteral> JNumber =
            from integer in JInt
            from frac in JFrac.Optional()
            from exp in JExp.Optional()
            select new JSONLiteral(integer +
                                   (frac.IsDefined ? frac.Get() : "") +
                                   (exp.IsDefined ? exp.Get() : ""),
                                   LiteralType.Number);

        static readonly Parser<JSONDate> JDate =
            from begin in Parse.String("new Date(")
            from year in JInt
            from comma1 in Parse.Char(',')
            from month in JInt
            from comma2 in Parse.Char(',')
            from day in JInt
            from end in Parse.String(")")
            select new JSONDate(year, month, day);


        static List<char> EscapeChars = new List<char> { '\"', '\\', 'b', 'f', 'n', 'r', 't' };

        static Parser<U> EnumerateInput<T, U>(T[] input, Func<T, Parser<U>> parser)
        {
            if (input == null || input.Length == 0) throw new ArgumentNullException("input");
            if (parser == null) throw new ArgumentNullException("parser");

            return i =>
            {
                foreach (var inp in input)
                {
                    var res = parser(inp)(i);
                    if (res.WasSuccessful) return res;
                }

                return Result.Failure<U>(null, null, null);
            };
        }

        /// <summary>
        /// Parses a control char, which is a character preceded by the escape character '\'
        /// </summary>
        static readonly Parser<char> ControlChar =
            from first in Parse.Char('\\')
            from next in EnumerateInput(EscapeChars.ToArray(), c => Parse.Char(c))
            select ((next == 't') ? '\t' :
                    (next == 'r') ? '\r' :
                    (next == 'n') ? '\n' :
                    (next == 'f') ? '\f' :
                    (next == 'b') ? '\b' :
                    next);

        /// <summary>
        /// Parses a JSON character
        /// </summary>
        static readonly Parser<char> JCharForSingleQuotedString = Parse.AnyChar.Except(Parse.Char('\'').Or(Parse.Char('\\'))).Or(ControlChar);
        static readonly Parser<char> JCharForDoubleQuotedString = Parse.AnyChar.Except(Parse.Char('"').Or(Parse.Char('\\'))).Or(ControlChar);
        static readonly Parser<char> JCharForParentesisExpression = Parse.AnyChar.Except(Parse.Char(')'));

        static readonly Parser<char> JCharForUnquotedString = Parse.Char((c =>
            char.IsLetter(c) ||
            char.IsDigit(c) ||
            c == '_' ||
            c == '.'), "unquoted chars"
        );

        static readonly Parser<JSONLiteral> JParentesisExpression =
            from first in Parse.Char('(')
            from chars in JCharForParentesisExpression.Many().Text()
            from last in Parse.Char(')')
            select new JSONLiteral(chars, LiteralType.String);


        static readonly Parser<char> JKeyChar = Parse.Char(c => char.IsLetter(c) || char.IsDigit(c) || c == '_', "key character");


        /// <summary>
        /// Parses a JSON string
        /// </summary>
        static readonly Parser<JSONLiteral> JString =
            Parse.Ref(() => JDoubleQuotedString
                .XOr(JSingleQuotedString)
                .XOr(JUnquotedString)
            );

        static readonly Parser<JSONLiteral> JSingleQuotedString =
            from first in Parse.Char('\'')
            from chars in JCharForSingleQuotedString.Many().Text()
            from last in Parse.Char('\'')
            select new JSONLiteral(chars, LiteralType.String);

        static readonly Parser<JSONLiteral> JDoubleQuotedString =
            from first in Parse.Char('"')
            from chars in JCharForDoubleQuotedString.Many().Text()
            from last in Parse.Char('"')
            select new JSONLiteral(chars, LiteralType.String);

        static readonly Parser<JSONLiteral> JUnquotedString =
            from chars in JCharForUnquotedString.Many().Text()
            from parens in JParentesisExpression.Optional()
            select new JSONLiteral(chars + (parens.IsDefined ? $"({parens.Get().Value})" : null), LiteralType.String);


        static readonly Parser<JSONLiteral> JKey =
            from chars in JKeyChar.Many().Text()
            select new JSONLiteral(chars, LiteralType.String);

        /// <summary>
        /// Parses any literal JSON value: string, number, boolean, null
        /// </summary>
        static readonly Parser<JSONLiteral> JLiteral =
            JString
            .XOr(JNumber)
            .XOr(JBoolean)
            .XOr(JNull);

        /// <summary>
        /// Parses any JSON value
        /// </summary>
        static readonly Parser<IJSONValue> JValue =
            Parse.Ref(() => JObject)
            .Or(Parse.Ref(() => JArray))
            .Or(JDate)
            .Or(JLiteral);

        /// <summary>
        /// Parses the elements within a JSON array
        /// </summary>
        static readonly Parser<IEnumerable<IJSONValue>> JElements = JValue.DelimitedBy(Parse.Char(',').Token());

        /// <summary>
        /// Parses a JSON array
        /// </summary>
        static readonly Parser<IJSONValue> JArray =
            from first in Parse.Char('[').Token()
            from elements in JElements.Optional()
            from last in Parse.Char(']').Token()
            select new JSONArray(elements.IsDefined ? elements.Get() : null);

        /// <summary>
        /// Parses a JSON pair
        /// </summary>
        static readonly Parser<KeyValuePair<string, IJSONValue>> JPair =
            from name in JKey
            from colon in Parse.Char(':').Token()
            from val in JValue
            select new KeyValuePair<string, IJSONValue>(name.Value, val);

        /// <summary>
        /// Parses all the pairs (members) of a JSON object
        /// </summary>
        static readonly Parser<IEnumerable<KeyValuePair<string, IJSONValue>>> JMembers = JPair.DelimitedBy(Parse.Char(',').Token());

        /// <summary>
        /// Parses a JSON object
        /// </summary>
        static readonly Parser<IJSONValue> JObject =
            from first in Parse.Char('{').Token()
            from members in JMembers.Optional()
            from last in Parse.Char('}').Token()
            select new JSONObject(members.IsDefined ? members.Get() : null);

        /// <summary>
        /// Parses a JObject
        /// </summary>
        /// <param name="toParse">The text to parse</param>
        /// <returns>A IJSONValue cast as a JSONObject</returns>
        public static JSONObject ParseJSON(string toParse)
        {
            return (JSONObject)JObject.Parse(toParse);
        }
    }
}
