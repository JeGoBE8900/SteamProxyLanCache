# SteamProxyLanCache

A little project to make myself easy to redownload way faster from Steam<br/>

![image](https://github.com/user-attachments/assets/f92530d9-1c45-4e5b-b564-a97615c8dd9d)

![image](https://github.com/user-attachments/assets/b54a10b8-81e0-498a-a218-02d21c03ddef)

![image](https://github.com/user-attachments/assets/eb62faba-0ddc-48a9-a26f-a432a4e31525)

![image](https://github.com/user-attachments/assets/2a2c5c8d-e5a6-4f51-8c47-6a9eed73c9cc)


![image](https://github.com/user-attachments/assets/18c38f32-ebb6-43f7-a868-5a707be2c125)


Caches steam files locally. When redownload from steam, checks the cache first.<br/>

Steam download servers can be added in steamservers.json.<br/>
Config can be changed in settings.json or UI.<br/>
You can filter by steamdepot number, eg. 123456;789123. You can place a * to allow caching of all depots.<br/>

This tool runs with a httpListener on port 80.<br/><br/>

Host file of your OS should include 127.0.0.1	lancache.steamcontent.com or,<br/>
adjust your DNS server for lancache.steamcontent.com to the IP address of the computer runnning this tool.

Make sure firewalls are able to pass the traffic.<br/>

Maybe this is needed too, run as elevated in dos prompt:<br/>
netsh http add urlacl url=http://+:80/ user=DOMAIN\user <br/>
See here: http://msdn.microsoft.com/en-us/library/ms733768.aspx

To support this tool, https://www.paypal.com/donate/?hosted_button_id=XKHD6S28ZUPKC
