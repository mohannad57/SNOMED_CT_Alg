﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using QuickType;
//
//    var searchQuery = SearchQuery.FromJson(jsonString);

namespace SNOMEDIDSelector.Models
{
    public partial class SearchQuery
    {
        [JsonProperty("page")]
        public long Page { get; set; }

        [JsonProperty("pageCount")]
        public long PageCount { get; set; }

        [JsonProperty("totalCount")]
        public long TotalCount { get; set; }

        [JsonProperty("prevPage")]
        public object PrevPage { get; set; }

        [JsonProperty("nextPage")]
        public object NextPage { get; set; }

        [JsonProperty("collection")]
        public Collection[] Collection { get; set; }
    }

    public partial class SearchQuery
    {
        public static SearchQuery FromJson(string json) => JsonConvert.DeserializeObject<SearchQuery>(json, Misc.Converter.Settings);
    }

}
