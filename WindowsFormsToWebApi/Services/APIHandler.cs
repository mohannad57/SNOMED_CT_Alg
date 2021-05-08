using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using Newtonsoft.Json;
using System.Reflection;
using SNOMEDIDSelector.Models;
using Mono.Web;

namespace SNOMEDIDSelector.Services
{
    public class APIHandler
    {
        public static HttpClient Client { get; set; }
        private static readonly string BioPortal_API_Key = "ad1195a5-bd37-4523-b787-6906a099791a";
        private static readonly string Base_Search_URI = @"http://data.bioontology.org/search";
        private static readonly string Base_Class_URI = @"http://data.bioontology.org/ontologies/SNOMEDCT/classes/";

        public static SearchQuery GetBioPortalSearch(string query, bool require_exact_match=true)
        {
            Client = new HttpClient();
            SearchQuery dataObjects;
            try
            {
                // Assign the base address 
                Client.BaseAddress = new Uri(Base_Search_URI);
                // Add an Accept header for JSON format.
                Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                // List data arguments.
                string args = "?";
                args += "q=" + query;
                args += "&require_exact_match=" + (require_exact_match?"true":"false");
                args += "&ontologies=" + "SNOMEDCT";
                args += "&include=" + "properties";
                args += "&apikey=" + BioPortal_API_Key;
                HttpResponseMessage response = Client.GetAsync(args).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
                
                if (response.IsSuccessStatusCode)
                {
                    // Parse the response body.
                    var resType = response.Content.GetType();
                    dataObjects = JsonConvert.DeserializeObject<SearchQuery>(response.Content.ReadAsStringAsync().Result);  //Make sure to add a reference to System.Net.Http.Formatting.dll

                    return dataObjects;
                }
                else
                {
                    ErrorHandler.ERRORS.Add(new Error() { Code = response.StatusCode.ToString(), Msg = response.ReasonPhrase, Method = MethodBase.GetCurrentMethod().Name });
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        internal static Children getBioPortalChildren(string childrenLink)
        {
            Client = new HttpClient();
            Children dataObjects;
            try
            {
                // Assign the base address 
                Client.BaseAddress = new Uri(childrenLink);
                // Add an Accept header for JSON format.
                Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                // List data arguments.
                string args = "?";
                args += "&apikey=" + BioPortal_API_Key;
                HttpResponseMessage response = Client.GetAsync(args).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.

                if (response.IsSuccessStatusCode)
                {
                    // Parse the response body.
                    var resType = response.Content.GetType();
                    dataObjects = JsonConvert.DeserializeObject<Children>(response.Content.ReadAsStringAsync().Result);  //Make sure to add a reference to System.Net.Http.Formatting.dll
                    if (dataObjects.Page < dataObjects.PageCount)
                    {
                        Collection[] coll = getBioPortalChildrenNextPage(dataObjects.Links.NextPage.ToString()).Collection;
                        List<Collection> newCollection = new List<Collection>();
                        newCollection.AddRange(dataObjects.Collection);
                        newCollection.AddRange(coll);
                        dataObjects.Collection = newCollection.ToArray();
                    }
                    return dataObjects;
                }
                else
                {
                    ErrorHandler.ERRORS.Add(new Error() { Code = response.StatusCode.ToString(), Msg = response.ReasonPhrase, Method = MethodBase.GetCurrentMethod().Name });
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        internal static Children getBioPortalChildrenNextPage(string nextPageURL)
        {
            Client = new HttpClient();
            Children dataObjects;
            try
            {
                // Assign the base address 
                //Client.BaseAddress = new Uri(nextPageURL);
                // Add an Accept header for JSON format.
                Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                // List data arguments.
                //string args = "?";
                //args += "&apikey=" + BioPortal_API_Key;
                HttpResponseMessage response = Client.GetAsync(nextPageURL).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.

                if (response.IsSuccessStatusCode)
                {
                    // Parse the response body.
                    var resType = response.Content.GetType();
                    dataObjects = JsonConvert.DeserializeObject<Children>(response.Content.ReadAsStringAsync().Result);  //Make sure to add a reference to System.Net.Http.Formatting.dll
                    if (dataObjects.Page < dataObjects.PageCount)
                    {
                        Collection[] coll = getBioPortalChildrenNextPage(dataObjects.Links.NextPage.ToString()).Collection;
                        List<Collection> newCollection = new List<Collection>();
                        newCollection.AddRange(dataObjects.Collection);
                        newCollection.AddRange(coll);
                        dataObjects.Collection = newCollection.ToArray();
                    }                      
                    return dataObjects;
                }
                else
                {
                    ErrorHandler.ERRORS.Add(new Error() { Code = response.StatusCode.ToString(), Msg = response.ReasonPhrase, Method = MethodBase.GetCurrentMethod().Name });
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        internal static int GetBioPortalChildrenCount(string cls)
        {
            Client = new HttpClient();
            SearchClass dataObjects;
            try
            {
                string EncUrl = HttpUtility.UrlEncode(cls);
                // Assign the base address 
                Client.BaseAddress = new Uri(Base_Class_URI + EncUrl + "/children");
                // Add an Accept header for JSON format.
                Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                // List data arguments.
                string args = "?";
                args += "&apikey=" + BioPortal_API_Key;
                args += "&page=5";
                HttpResponseMessage response = Client.GetAsync(args).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.

                if (response.IsSuccessStatusCode)
                {
                    // Parse the response body.
                    var resType = response.Content.GetType();
                    dataObjects = JsonConvert.DeserializeObject<SearchClass>(response.Content.ReadAsStringAsync().Result);  //Make sure to add a reference to System.Net.Http.Formatting.dll

                    return dataObjects.TotalCount;
                }
                else
                {
                    ErrorHandler.ERRORS.Add(new Error() { Code = response.StatusCode.ToString(), Msg = response.ReasonPhrase, Method = MethodBase.GetCurrentMethod().Name });
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        internal static SearchClass GetBioPortalClass(string obj)
        {
            Client = new HttpClient();
            SearchClass dataObjects;
            try
            {
                string EncUrl = HttpUtility.UrlEncode(obj);
                // Assign the base address 
                Client.BaseAddress = new Uri(Base_Class_URI+ EncUrl);
                // Add an Accept header for JSON format.
                Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                // List data arguments.
                string args = "?";
                args += "&apikey=" + BioPortal_API_Key;
                HttpResponseMessage response = Client.GetAsync(args).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.

                if (response.IsSuccessStatusCode)
                {
                    // Parse the response body.
                    var resType = response.Content.GetType();
                    dataObjects = JsonConvert.DeserializeObject<SearchClass>(response.Content.ReadAsStringAsync().Result);  //Make sure to add a reference to System.Net.Http.Formatting.dll

                    return dataObjects;
                }
                else
                {
                    ErrorHandler.ERRORS.Add(new Error() { Code = response.StatusCode.ToString(), Msg = response.ReasonPhrase, Method = MethodBase.GetCurrentMethod().Name });
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
