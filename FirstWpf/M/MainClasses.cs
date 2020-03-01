using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Xml;
using System.IO;
using System.Threading;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Collections.ObjectModel;

namespace FirstWpf
{
    class MainClasses
    {
        public static string GetFromURL(string address)
        {
            using (var client = new WebClient())
            {
                return Encoding.UTF8.GetString(client.DownloadData(address));
            }
        }

        public static BitmapImage GetImage(string SteamID)
        {
            var buffer = new WebClient().DownloadData(SteamID);
            var bitmap = new BitmapImage();

            using (var stream = new MemoryStream(buffer))
            {
                bitmap.BeginInit();
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.StreamSource = stream;
                bitmap.EndInit();
            }

            return bitmap;
        }
        public static void RemoveFromList(string SteamID)
        {
            string URL = "https://steamcommunity.com/profiles/" + SteamID + "/";

            StreamReader HackerListR = new StreamReader(InfoPath.GetHackerListPath());
            List<string> List = new List<string>();
            while (!HackerListR.EndOfStream)
            {
                List.Add(HackerListR.ReadLine());
            }
            HackerListR.Close();

            StreamWriter HackerListW = new StreamWriter(InfoPath.GetHackerListPath());
            foreach (string line in List)
            {
                if (!new Regex(URL).IsMatch(line)) HackerListW.WriteLine(line);
            }
            HackerListW.Close();
        }

        public static void AddToList(string SteamID)
        {
            string URL = "https://steamcommunity.com/profiles/" + SteamID + "/";

            StreamReader HackerListR = new StreamReader(InfoPath.GetHackerListPath());
            List<string> List = new List<string>();
            while (!HackerListR.EndOfStream)
            {
                List.Add(HackerListR.ReadLine());
            }
            HackerListR.Close();

            StreamWriter HackerListW = new StreamWriter(InfoPath.GetHackerListPath());
            foreach (string line in List)
            {
                HackerListW.WriteLine(line);
            }
            HackerListW.WriteLine(URL);
            HackerListW.Close();
        }

        public static string FindIdInString(string String)
        {
            string Res = String;
            int x = Res.Length;
            for (int i = 0; i < x - 17; i++)
            {
                if (Res.Substring(0, 3) == "765")
                {
                    return Res.Substring(0, 17);
                }
                Res = Res.Remove(0, 1);
            }
            return "";
        }

        public static XmlDocument GetXmlFromUrl(string SteamID)
        {
            string URL;

            if (SteamID == "") throw new Exception("Оно пустое.-.");

            if (new Regex("765").IsMatch(SteamID))
            {
                URL = "https://steamcommunity.com/profiles/" + SteamID + "/?xml=1";
            }
            else
            {
                URL = "https://steamcommunity.com/id/" + SteamID + "/?xml=1";
            }
            var result = new XmlDocument();
            result.LoadXml(GetFromURL(URL));
            return result;

        }

        public static PlayerInfo GetPlayerInfo(string SteamID)
        {
            var SteamAPIXMl = GetXmlFromUrl(SteamID);
            PlayerInfo HackerInfo = new PlayerInfo();

            try
            {
                foreach (XmlNode xmlNode in SteamAPIXMl.DocumentElement.ChildNodes)
                {
                    switch (xmlNode.Name)
                    {
                        case "steamID":
                            HackerInfo.Name = xmlNode.InnerText;
                            break;

                        case "steamID64":
                            HackerInfo.ID = xmlNode.InnerText;
                            break;

                        case "vacBanned":
                            HackerInfo.VAC = Convert.ToInt32(xmlNode.InnerText);
                            break;

                        case "stateMessage":
                            HackerInfo.GameStatus = xmlNode.InnerText;
                            break;

                        case "avatarMedium":
                            HackerInfo.Avatar = GetImage(xmlNode.InnerText);
                            HackerInfo.Avatar.Freeze();
                            break;
                    }
                }
            }

            catch (Exception ex)
            {
                return new PlayerInfo(ex.Message, FindIdInString(SteamID), 0, "Error", new BitmapImage());
            }

            if (!HackerInfo.Avatar.IsFrozen)
            {
                HackerInfo.Avatar = new BitmapImage(); HackerInfo.Avatar.Freeze();
            }
            return HackerInfo;
        }

        public static List<PlayerInfo> GetAll()
        {
            var playerInfos = new List<PlayerInfo>();

            var HackerListStream = new StreamReader(InfoPath.GetHackerListPath());

            var PlayerList = new List<string>();

            while (!HackerListStream.EndOfStream)
            {
                PlayerList.Add(HackerListStream.ReadLine());
            }

            Parallel.ForEach(PlayerList, (current) =>
            {
                playerInfos.Add(GetPlayerInfo(FindIdInString(current)));
            });

            HackerListStream.Close();
            return playerInfos;
        }
        public static async Task<List<PlayerInfo>> GetAllAsync()
        {
            var result = await Task.Run(() => GetAll());
            return result;
        }

        public static List<PlayerInfo> GetOnline()
        {
            var PlayerInfos = new List<PlayerInfo>();

            var PlayerList = new List<string>();

            var HackerListSteam = new StreamReader(InfoPath.GetHackerListPath());

            while (!HackerListSteam.EndOfStream)
            {
                PlayerList.Add(HackerListSteam.ReadLine());
            }

            Parallel.ForEach(PlayerList, (current) =>
            {
                var currentPlayer = GetPlayerInfo(FindIdInString(current));

                if (currentPlayer.GameStatus != "Offline")
                {
                    PlayerInfos.Add(currentPlayer);
                }
            });

            HackerListSteam.Close();
            return PlayerInfos;
        }
        public static async Task<List<PlayerInfo>> GetOnlineAsync()
        {
            var result = await Task.Run(() => GetOnline());
            return result;
        }
    }
}
