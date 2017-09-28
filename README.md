# PlexWebhook-To-SlackWebhook-CSharp
C# webApi for receiving and handling Plex webhook posts and then sending a Slack webhook.

This repo is a C# version of my previous Repository
* [Khaoz-Topsy/PlexWebhook-To-SlackWebhook](https://github.com/Khaoz-Topsy/PlexWebhook-To-SlackWebhook)


### Installation

Change the SlackUrl setting in the web.config
Build and publish

### Testing
Build and Run
Use Postman to post to localhost:{port}/api/v0/Slack/Default.
- in the body choose "form-data"
- add the key "payload"
- add the contents of TestData.json to the value