using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PlexWebhook.Data.Repository
{
    public class SlackRepository
    {
        public string SendMessage(string webHookURL, string msgContent)
        {
            string Result = string.Empty;

            try
            {
                WebRequest tRequest = WebRequest.Create(webHookURL);
                tRequest.Method = "post";
                tRequest.ContentType = "application/json";

                //Byte[] byteArray = Encoding.UTF8.GetBytes(json);
                Byte[] byteArray = Encoding.UTF8.GetBytes(msgContent);
                tRequest.ContentLength = byteArray.Length;
                using (Stream dataStream = tRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    using (WebResponse tResponse = tRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = tResponse.GetResponseStream())
                        {
                            using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                Result = tReader.ReadToEnd();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Result = string.Format("Slack exception: {0}", ex.Message);
            }

            return Result;
        }
    }
}