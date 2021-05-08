using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace SNOMEDIDSelector.Services
{
    public static class ErrorHandler
    {
        public static List<Error> ERRORS { get; set; } = new List<Error>();
        public static void ResetErrors()
        {
            ERRORS = new List<Error>();
        }
        public static void LogErrors()
        {
            try
            {
                if (ErrorHandler.ERRORS.Count > 0)
                    File.AppendAllText("ERRORS.txt", JsonConvert.SerializeObject(ErrorHandler.ERRORS));
                    //File.AppendAllText(Config.OUT_FILE_ErrorLog, JsonConvert.SerializeObject(ErrorHandler.ERRORS));
            }
            catch (Exception ex)
            {
                File.AppendAllText($@"ErrorLog.log", JsonConvert.SerializeObject(ErrorHandler.ERRORS));
                //File.AppendAllText($@"{Config.WDir}Logs\ErrorLog_{Config.CurrSimName}_{Config.RunTimestamp}_2.log", JsonConvert.SerializeObject(ErrorHandler.ERRORS));
            }
            //ERRORS.Clear();
        }
    }

    public class Error
    {
        [XmlAttribute(AttributeName = "Code")]
        public string Code { get; set; }
        [XmlAttribute(AttributeName = "Msg")]
        public string Msg { get; set; }
        [XmlAttribute(AttributeName = "Method")]
        public string Method { get; set; }
        [XmlAttribute(AttributeName = "Record")]
        public string Record { get; set; }
    }
}