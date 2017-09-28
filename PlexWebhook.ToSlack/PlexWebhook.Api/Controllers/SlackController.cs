using Newtonsoft.Json;
using PlexWebhook.Data.Repository;
using PlexWebhook.Domain.Mapper;
using PlexWebhook.Domain.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace PlexWebhook.Api.Controllers
{
    public class SlackController : ApiController
    {

        [HttpPost] // api/v0/Slack/Default
        [Route("api/v0/Slack/Default")]
        public async Task<string> Default()
        {
            var multiPart = await Request.Content.ReadAsMultipartAsync();
            var requestJson = await multiPart.Contents[0].ReadAsStringAsync();

            PlexMessage requestObject = JsonConvert.DeserializeObject<PlexMessage>(requestJson);

            if (requestObject != null)
            {
                if (requestObject.IsActionable())
                {
                    var messageContent = new
                    {
                        icon_emoji = ":plex:",
                        attachments = new[]
                        {
                            new
                            {
                                fallback = "Required plain-text summary of the attachment.",
                                color = "#a67a2d",
                                title = requestObject.CreateTitle(),
                                text = $"{requestObject.Account.title} at ip: { requestObject.Player.publicAddress}",
                                thumb_url = "http://khaoznet.xyz/host/Pictures/3.png",
                                footer = requestObject.GetFooter(),
                                footer_icon = requestObject.Account.thumb
                            }
                        }
                    };

                    string slackUrl = System.Web.Configuration.WebConfigurationManager.AppSettings["SlackUrl"];
                    string messageContentJSON = JsonConvert.SerializeObject(messageContent);
                    SlackRepository slackRepo = new SlackRepository();
                    slackRepo.SendMessage(slackUrl, messageContentJSON);

                }
            }

            return "Yay";
        }



    }
}
