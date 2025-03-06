# SteamProxyLanCahe

A little project to make myself easy to redownload way faster from Steam<br/>

![image](https://github.com/user-attachments/assets/379d39d8-45ee-4df7-8716-49006492ce14)


Caches steam files locally. When redownload from steam, checks the cache first.<br/>

Steam download servers can be added in steamservers.json.<br/>
Config can be changed in settings.json or UI.<br/>
You can filter by steamdepot number, eg. 123456;789123. U can place a * to allow caching of all depots.<br/>

This tool runs with a httpListener on port 80.<br/><br/>

Host file of your OS should include 127.0.0.1	lancache.steamcontent.com or,<br/>
adjust your DNS server for lancache.steamcontent.com to the IP address of the computer runnning this tool.

Make sure firewalls are able to pass the traffic.<br/>

Maybe this is needed too, run as elevated in dos prompt:<br/>
netsh http add urlacl url=http://+:80/MyUri user=DOMAIN\user <br/>
See here: http://msdn.microsoft.com/en-us/library/ms733768.aspx

To support this tool, https://www.paypal.com/donate/?hosted_button_id=XKHD6S28ZUPKC
