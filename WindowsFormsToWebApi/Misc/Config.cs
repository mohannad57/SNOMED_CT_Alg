using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SNOMEDIDSelector.Misc
{
    public static class Config
    {
        internal static string URI_BASE_id = @"http://purl.bioontology.org/ontology/SNOMEDCT/";
        internal static string URI_prefLabel = @"http://www.w3.org/2004/02/skos/core#prefLabel";
        internal static string URI_BASE_has_properti = @"http://purl.bioontology.org/ontology/SNOMEDCT/has_";
        internal static HashSet<string> URI_BASE_properties = new HashSet<string>
        {
            @"http://purl.bioontology.org/ontology/SNOMEDCT/due_to",
            @"http://purl.bioontology.org/ontology/SNOMEDCT/occurs_after",
            @"http://purl.bioontology.org/ontology/SNOMEDCT/during",
            @"http://purl.bioontology.org/ontology/SNOMEDCT/before",
            @"http://purl.bioontology.org/ontology/SNOMEDCT/occurs_in",
            @"http://purl.bioontology.org/ontology/SNOMEDCT/uses_access_device",
            @"http://purl.bioontology.org/ontology/SNOMEDCT/uses_device",
            @"http://purl.bioontology.org/ontology/SNOMEDCT/has_indirect_device",
            @"http://purl.bioontology.org/ontology/SNOMEDCT/has_direct_device",
            @"http://purl.bioontology.org/ontology/SNOMEDCT/interprets",
            //@"http://purl.bioontology.org/ontology/SNOMEDCT/",
            //@"http://purl.bioontology.org/ontology/SNOMEDCT/",
            //@"http://purl.bioontology.org/ontology/SNOMEDCT/",
            //@"http://purl.bioontology.org/ontology/SNOMEDCT/",
            //@"http://purl.bioontology.org/ontology/SNOMEDCT/",
            //@"http://purl.bioontology.org/ontology/SNOMEDCT/",
            //@"http://purl.bioontology.org/ontology/SNOMEDCT/",
            //@"http://purl.bioontology.org/ontology/SNOMEDCT/",
            //@"http://purl.bioontology.org/ontology/SNOMEDCT/",
        };
    }
}