# SteamProxyLanCahe

A little project to make myself easy to redownload way faster from Steam

Caches steam files locally. When redownload from steam, checks the cache first.

Steam download servers can be added in steamservers.json.
Config can be changed in settings.json or UI.
You can filter by steamdepot number, eg. 123456;789123. U can place a * to allow caching of all depots.

To support this tool, https://www.paypal.com/donate/?hosted_button_id=XKHD6S28ZUPKC


This tool runs with a httpListener on port 80.

Host file of your OS should include 127.0.0.1	lancache.steamcontent.com or,
adjust your DNS server for lancache.steamcontent.com to the IP address of the computer runnning this tool.

Make sure firewalls are able to pass the traffic.

Maybe this is needed too, run as elevated in dos prompt:
netsh http add urlacl url=http://+:80/MyUri user=DOMAIN\user

See here: http://msdn.microsoft.com/en-us/library/ms733768.aspx which shows this:

