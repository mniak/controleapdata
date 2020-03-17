using Newtonsoft.Json;
using System;

namespace ApdataTimecardFixer.Client.Models
{
    public class GetEditGridPageResponseModel
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("recCount")]
        public long RecCount { get; set; }

        [JsonProperty("recs")]
        public Rec[] Recs { get; set; }
    }

    public class Rec
    {
        [JsonProperty("Field_0")]
        public bool Field0 { get; set; }

        [JsonProperty("Field_1")]
        public string Field1 { get; set; }

        [JsonProperty("Field_2")]
        public long Field2 { get; set; }

        [JsonProperty("Field_3")]
        public TipoDeDia Tipo { get; set; }

        [JsonProperty("Field_4")]
        public string Field4 { get; set; }

        [JsonProperty("Field_5")]
        public string Field5 { get; set; }

        [JsonProperty("Field_6")]
        public long Field6 { get; set; }

        [JsonProperty("Field_7")]
        public string Entrada1 { get; set; }

        [JsonProperty("Field_8")]
        public long Field8 { get; set; }

        [JsonProperty("Field_9")]
        public long Field9 { get; set; }

        [JsonProperty("Field_10")]
        public long Field10 { get; set; }

        [JsonProperty("Field_11")]
        public long Field11 { get; set; }

        [JsonProperty("Field_12")]
        public long Field12 { get; set; }

        [JsonProperty("Field_13")]
        public long Field13 { get; set; }

        [JsonProperty("Field_14")]
        public string Field14 { get; set; }

        [JsonProperty("Field_15")]
        public string Saida1 { get; set; }

        [JsonProperty("Field_16")]
        public long Field16 { get; set; }

        [JsonProperty("Field_17")]
        public long Field17 { get; set; }

        [JsonProperty("Field_18")]
        public long Field18 { get; set; }

        [JsonProperty("Field_19")]
        public long Field19 { get; set; }

        [JsonProperty("Field_20")]
        public long Field20 { get; set; }

        [JsonProperty("Field_21")]
        public long Field21 { get; set; }

        [JsonProperty("Field_22")]
        public string Field22 { get; set; }

        [JsonProperty("Field_23")]
        public string Field23 { get; set; }

        [JsonProperty("Field_24")]
        public long Field24 { get; set; }

        [JsonProperty("Field_25")]
        public long Field25 { get; set; }

        [JsonProperty("Field_26")]
        public long Field26 { get; set; }

        [JsonProperty("Field_27")]
        public long Field27 { get; set; }

        [JsonProperty("Field_28")]
        public long Field28 { get; set; }

        [JsonProperty("Field_29")]
        public long Field29 { get; set; }

        [JsonProperty("Field_30")]
        public string Field30 { get; set; }

        [JsonProperty("Field_31")]
        public string Field31 { get; set; }

        [JsonProperty("Field_32")]
        public long Field32 { get; set; }

        [JsonProperty("Field_33")]
        public long Field33 { get; set; }

        [JsonProperty("Field_34")]
        public long Field34 { get; set; }

        [JsonProperty("Field_35")]
        public long Field35 { get; set; }

        [JsonProperty("Field_36")]
        public long Field36 { get; set; }

        [JsonProperty("Field_37")]
        public long Field37 { get; set; }

        [JsonProperty("Field_38")]
        public string Field38 { get; set; }

        [JsonProperty("Field_39")]
        public string Field39 { get; set; }

        [JsonProperty("Field_40")]
        public long Field40 { get; set; }

        [JsonProperty("Field_41")]
        public long Field41 { get; set; }

        [JsonProperty("Field_42")]
        public long Field42 { get; set; }

        [JsonProperty("Field_43")]
        public long Field43 { get; set; }

        [JsonProperty("Field_44")]
        public long Field44 { get; set; }

        [JsonProperty("Field_45")]
        public long Field45 { get; set; }

        [JsonProperty("Field_46")]
        public string Field46 { get; set; }

        [JsonProperty("Field_47")]
        public string Field47 { get; set; }

        [JsonProperty("Field_48")]
        public long Field48 { get; set; }

        [JsonProperty("Field_49")]
        public long Field49 { get; set; }

        [JsonProperty("Field_50")]
        public long Field50 { get; set; }

        [JsonProperty("Field_51")]
        public long Field51 { get; set; }

        [JsonProperty("Field_52")]
        public long Field52 { get; set; }

        [JsonProperty("Field_53")]
        public long Field53 { get; set; }

        [JsonProperty("Field_54")]
        public string Field54 { get; set; }

        [JsonProperty("Field_55")]
        public string Field55 { get; set; }

        [JsonProperty("Field_56")]
        public long Field56 { get; set; }

        [JsonProperty("Field_57")]
        public long Field57 { get; set; }

        [JsonProperty("Field_58")]
        public long Field58 { get; set; }

        [JsonProperty("Field_59")]
        public long Field59 { get; set; }

        [JsonProperty("Field_60")]
        public long Field60 { get; set; }

        [JsonProperty("Field_61")]
        public long Field61 { get; set; }

        [JsonProperty("Field_62")]
        public string Field62 { get; set; }

        [JsonProperty("Field_63")]
        public string Field63 { get; set; }

        [JsonProperty("Field_64")]
        public long Field64 { get; set; }

        [JsonProperty("Field_65")]
        public long Field65 { get; set; }

        [JsonProperty("Field_66")]
        public long Field66 { get; set; }

        [JsonProperty("Field_67")]
        public long Field67 { get; set; }

        [JsonProperty("Field_68")]
        public long Field68 { get; set; }

        [JsonProperty("Field_69")]
        public long Field69 { get; set; }

        [JsonProperty("Field_70")]
        public string Field70 { get; set; }

        [JsonProperty("Field_71")]
        public string Field71 { get; set; }

        [JsonProperty("Field_72")]
        public DateTime Field72 { get; set; }

        [JsonProperty("Field_73")]
        public string Field73 { get; set; }

        [JsonProperty("Field_74")]
        public StatusDoDia Status { get; set; }
    }
}