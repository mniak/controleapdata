namespace ApdataTimecardFixer.Client.Models
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using System;
    using System.Globalization;

    public class CreateEditGridAndGetHeadersResponseModel
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("hwd")]
        public long Hwd { get; set; }

        [JsonProperty("isGetParams")]
        public bool IsGetParams { get; set; }

        [JsonProperty("groups")]
        public string[] Groups { get; set; }

        [JsonProperty("columns")]
        public Column[] Columns { get; set; }

        [JsonProperty("transactions")]
        public Transaction[] Transactions { get; set; }

        [JsonProperty("relationedFields")]
        public long[] RelationedFields { get; set; }

        [JsonProperty("reports")]
        public Report[] Reports { get; set; }

        [JsonProperty("usersReports")]
        public ItemsCanDo UsersReports { get; set; }

        [JsonProperty("itemsCanDo")]
        public ItemsCanDo ItemsCanDo { get; set; }
    }

    public class Column
    {
        [JsonProperty("header")]
        public string Header { get; set; }

        [JsonProperty("type")]
        public TypeEnum Type { get; set; }

        [JsonProperty("renderer", NullValueHandling = NullValueHandling.Ignore)]
        public string Renderer { get; set; }

        [JsonProperty("dataIndex")]
        public string DataIndex { get; set; }

        [JsonProperty("sortable")]
        public bool Sortable { get; set; }

        [JsonProperty("fieldId")]
        public long FieldId { get; set; }

        [JsonProperty("groupIndex")]
        public long GroupIndex { get; set; }

        [JsonProperty("obligatory")]
        public bool Obligatory { get; set; }

        [JsonProperty("mask")]
        public string Mask { get; set; }

        [JsonProperty("size")]
        public long Size { get; set; }

        [JsonProperty("defaultValue")]
        public string DefaultValue { get; set; }

        [JsonProperty("lookup")]
        public string Lookup { get; set; }

        [JsonProperty("keyIndex")]
        public long KeyIndex { get; set; }

        [JsonProperty("addLanguages")]
        public object[] AddLanguages { get; set; }

        [JsonProperty("readOnly")]
        public bool ReadOnly { get; set; }

        [JsonProperty("hidden")]
        public bool Hidden { get; set; }

        [JsonProperty("hint")]
        public string Hint { get; set; }

        [JsonProperty("readOnlyExcepts")]
        public object[] ReadOnlyExcepts { get; set; }

        [JsonProperty("line")]
        public long Line { get; set; }

        [JsonProperty("lookupEdit")]
        public LookupEdit LookupEdit { get; set; }

        [JsonProperty("isCep")]
        public bool IsCep { get; set; }

        [JsonProperty("isPhone")]
        public bool IsPhone { get; set; }

        [JsonProperty("flagImgSrc")]
        public string FlagImgSrc { get; set; }

        [JsonProperty("defaultUpdateValue")]
        public string DefaultUpdateValue { get; set; }

        [JsonProperty("campoCondicionador")]
        public bool CampoCondicionador { get; set; }

        [JsonProperty("acaoCampo")]
        public long AcaoCampo { get; set; }

        [JsonProperty("campoCondicionado")]
        public bool CampoCondicionado { get; set; }
    }

    public class LookupEdit
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("caption")]
        public string Caption { get; set; }
    }

    public class ItemsCanDo
    {
    }

    public class Report
    {
        [JsonProperty("key")]
        public long Key { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }

    public class Transaction
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("caption")]
        public string Caption { get; set; }

        [JsonProperty("action")]
        public Action Action { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("accessible")]
        public bool Accessible { get; set; }

        [JsonProperty("useDignalSign")]
        public bool UseDignalSign { get; set; }

        [JsonProperty("groupID")]
        public long GroupId { get; set; }

        [JsonProperty("iconID")]
        public long IconId { get; set; }

        [JsonProperty("captionAbrev")]
        public string CaptionAbrev { get; set; }

        [JsonProperty("recWFObs")]
        public bool RecWfObs { get; set; }

        [JsonProperty("requiredWFObs")]
        public bool RequiredWfObs { get; set; }
    }

    public enum TypeEnum { Boolean, Date, Int, String };

    public enum Action { PostEdit, SendKey };

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                TypeEnumConverter.Singleton,
                ActionConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class TypeEnumConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(TypeEnum) || t == typeof(TypeEnum?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "boolean":
                    return TypeEnum.Boolean;
                case "date":
                    return TypeEnum.Date;
                case "int":
                    return TypeEnum.Int;
                case "string":
                    return TypeEnum.String;
            }
            throw new Exception("Cannot unmarshal type TypeEnum");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (TypeEnum)untypedValue;
            switch (value)
            {
                case TypeEnum.Boolean:
                    serializer.Serialize(writer, "boolean");
                    return;
                case TypeEnum.Date:
                    serializer.Serialize(writer, "date");
                    return;
                case TypeEnum.Int:
                    serializer.Serialize(writer, "int");
                    return;
                case TypeEnum.String:
                    serializer.Serialize(writer, "string");
                    return;
            }
            throw new Exception("Cannot marshal type TypeEnum");
        }

        public static readonly TypeEnumConverter Singleton = new TypeEnumConverter();
    }

    internal class ActionConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Action) || t == typeof(Action?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "postEdit":
                    return Action.PostEdit;
                case "sendKey":
                    return Action.SendKey;
            }
            throw new Exception("Cannot unmarshal type Action");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Action)untypedValue;
            switch (value)
            {
                case Action.PostEdit:
                    serializer.Serialize(writer, "postEdit");
                    return;
                case Action.SendKey:
                    serializer.Serialize(writer, "sendKey");
                    return;
            }
            throw new Exception("Cannot marshal type Action");
        }

        public static readonly ActionConverter Singleton = new ActionConverter();
    }
}
