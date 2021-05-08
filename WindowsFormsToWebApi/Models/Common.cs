using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace SNOMEDIDSelector.Models
{
    public class Common
    {
    }


    //public partial class NavigationLinks
    //{
    //    [JsonProperty("nextPage")]
    //    public Uri NextPage { get; set; }

    //    [JsonProperty("prevPage")]
    //    public Uri PrevPage { get; set; }
    //}
    public partial class Context
    {
        [JsonProperty("@vocab")]
        public Uri Vocab { get; set; }

        [JsonProperty("prefLabel")]
        public Uri PrefLabel { get; set; }

        [JsonProperty("synonym")]
        public Uri Synonym { get; set; }

        [JsonProperty("definition")]
        public Uri Definition { get; set; }

        [JsonProperty("obsolete")]
        public Uri Obsolete { get; set; }

        [JsonProperty("semanticType")]
        public Uri SemanticType { get; set; }

        [JsonProperty("cui")]
        public Uri Cui { get; set; }
    }

    public partial class Links
    {
        [JsonProperty("nextPage")]
        public Uri NextPage { get; set; }

        [JsonProperty("prevPage")]
        public Uri PrevPage { get; set; }
        [JsonProperty("self")]
        public Uri Self { get; set; }

        [JsonProperty("ontology")]
        public Uri Ontology { get; set; }

        [JsonProperty("children")]
        public Uri Children { get; set; }

        [JsonProperty("parents")]
        public Uri Parents { get; set; }

        [JsonProperty("descendants")]
        public Uri Descendants { get; set; }

        [JsonProperty("ancestors")]
        public Uri Ancestors { get; set; }

        [JsonProperty("instances")]
        public Uri Instances { get; set; }

        [JsonProperty("tree")]
        public Uri Tree { get; set; }

        [JsonProperty("notes")]
        public Uri Notes { get; set; }

        [JsonProperty("mappings")]
        public Uri Mappings { get; set; }

        [JsonProperty("ui")]
        public Uri Ui { get; set; }

        [JsonProperty("@context", NullValueHandling = NullValueHandling.Ignore)]
        public Links Context { get; set; }
    }
    
    public partial class Collection
    {
        [JsonProperty("properties")]
        public Dictionary<string, string[]> Properties { get; set; }

        [JsonProperty("@id")]
        public Uri Id { get; set; }

        [JsonProperty("prefLabel")]
        public string PrefLabel { get; set; }

        [JsonProperty("synonym")]
        public string[] Synonym { get; set; }

        [JsonProperty("definition")]
        public object[] Definition { get; set; }

        [JsonProperty("@type")]
        public Uri Type { get; set; }

        [JsonProperty("links")]
        public ContextClass Links { get; set; }

        [JsonProperty("@context")]
        public Context Context { get; set; }
    }

    public partial class ContextClass
    {
        [JsonProperty("self")]
        public Uri Self { get; set; }

        [JsonProperty("ontology")]
        public Uri Ontology { get; set; }

        [JsonProperty("children")]
        public Uri Children { get; set; }

        [JsonProperty("parents")]
        public Uri Parents { get; set; }

        [JsonProperty("descendants")]
        public Uri Descendants { get; set; }

        [JsonProperty("ancestors")]
        public Uri Ancestors { get; set; }

        [JsonProperty("instances")]
        public Uri Instances { get; set; }

        [JsonProperty("tree")]
        public Uri Tree { get; set; }

        [JsonProperty("notes")]
        public Uri Notes { get; set; }

        [JsonProperty("mappings")]
        public Uri Mappings { get; set; }

        [JsonProperty("ui")]
        public Uri Ui { get; set; }

        [JsonProperty("@context", NullValueHandling = NullValueHandling.Ignore)]
        public ContextClass Context { get; set; }
    }

}