using PlexWebhook.Domain.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlexWebhook.Domain.Mapper
{
    public static class PlexMessageMapper
    {
        public static string CreateTitle(this PlexMessage plexM)
        {
            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrEmpty(plexM.Metadata.grandparentTitle)) { sb.Append(plexM.Metadata.grandparentTitle); sb.Append(" - "); };
            if (!string.IsNullOrEmpty(plexM.Metadata.parentTitle))      { sb.Append(plexM.Metadata.parentTitle); sb.Append(": "); };
            if (!string.IsNullOrEmpty(plexM.Metadata.title))            { sb.Append(plexM.Metadata.title); };
            
            return sb.ToString();
        }

        public static string g(this PlexMessage plexM)
        {
            //$"{action} by { requestObject.Account.title} on { requestObject.Player.title} from { requestObject.Server.title}",

            return "";
        }

        public static string GetAction(this PlexMessage plexM)
        {
            if (string.Equals(plexM.Event, "media.scrobble")) return "finished playing";
            if (string.Equals(plexM.Event, "media.play")) return "started playing";
            if (string.Equals(plexM.Event, "media.stop")) return "stopped playing";

            return "";
        }
        public static bool IsActionable(this PlexMessage plexM)
        {
            if (string.Equals(plexM.Event, "media.scrobble")) return true;
            if (string.Equals(plexM.Event, "media.play")) return true;
            if (string.Equals(plexM.Event, "media.stop")) return true;

            return false;
        }

        public static string GetFooter(this PlexMessage plexM)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(plexM.GetAction());
            sb.Append(" by ");

            sb.Append(plexM.Account.title);
            sb.Append(" on ");

            sb.Append(plexM.Player.title);
            sb.Append(" from ");

            sb.Append(plexM.Server.title);

            return sb.ToString();
        }

    }
}
