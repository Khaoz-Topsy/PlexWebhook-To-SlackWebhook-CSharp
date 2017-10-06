using Newtonsoft.Json;
using PlexWebhook.Data.Repository;
using PlexWebhook.Domain.Mapper;
using PlexWebhook.Domain.Store;
using System.Net.Http;
using System.Threading.Tasks;
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
            var requestObject = JsonConvert.DeserializeObject<PlexMessage>(requestJson);

            if (requestObject == null) return "Nay";
            if (!requestObject.IsActionable()) return "Nay";

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

            var slackUrl = System.Web.Configuration.WebConfigurationManager.AppSettings["SlackUrl"];
            var messageContentJson = JsonConvert.SerializeObject(messageContent);
            var slackRepo = new SlackRepository();
            slackRepo.SendMessage(slackUrl, messageContentJson);

            return "Yay";
        }



    }
}
