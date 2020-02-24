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
        public static string CutString(string String, int ResultLength)
        {
            string CopeString = String;

            if (CopeString.Length > ResultLength)
            {
                CopeString = CopeString.Substring(0, ResultLength);
            }
            CopeString = CopeString.PadRight(ResultLength);

            return CopeString;
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

        public static string DownloadStringFromURL(string URL)
        {
            try
            {
                using (var client = new WebClient())
                {
                    return Encoding.UTF8.GetString(client.DownloadData(new Uri(URL)));
                }
            }
            catch
            {
                throw new Exception("cannot download string");
            }
        }

        public static PlayerInfo GetPlayerInfo(string SteamID)
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

            XmlDocument SteamAPIXMl = new XmlDocument();
            SteamAPIXMl.LoadXml(DownloadStringFromURL(URL));

            PlayerInfo HackerInfo = new PlayerInfo();

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

                    case "avatarFull":
                        HackerInfo.Avatar = new BitmapImage(new Uri(xmlNode.InnerText));
                        break;
                }
            }
            return HackerInfo;
        }

        public static void UpdateAllPlayerInfo()
        {
            StreamReader HackerList = new StreamReader(InfoPath.GetHackerListPath());
            StreamWriter HackerInfo = new StreamWriter(InfoPath.GetHackerInfoPath());

            while (!HackerList.EndOfStream)
            {
                string id = FindIdInString(HackerList.ReadLine());
                PlayerInfo PlayerInfo;
                try
                {
                    PlayerInfo = GetPlayerInfo(id);
                }
                catch
                {
                    throw new Exception("cant get player info");
                }

                string RESULT = "https://steamcommunity.com/profiles/" + PlayerInfo.ID + "/" + " | " + PlayerInfo.ID + " | " + CutString(PlayerInfo.Name, 15) + " | " + PlayerInfo.VAC + " | " + PlayerInfo.GameStatus;
                HackerInfo.WriteLine(RESULT);
            }
            HackerInfo.Close();
            HackerList.Close();
        }

        public static ObservableCollection<PlayerInfo> GetAll()
        {
            ObservableCollection<PlayerInfo> playerInfos = new ObservableCollection<PlayerInfo>();

            StreamReader HackerList = new StreamReader(InfoPath.GetHackerListPath());

            while (!HackerList.EndOfStream)
            {
                try
                {
                    playerInfos.Add(GetPlayerInfo(FindIdInString(HackerList.ReadLine())));
                }
                catch
                {
                    playerInfos.Add(new PlayerInfo("Error","Error",0,"Error",new BitmapImage()));
                }
                
            }

            HackerList.Close();
            return playerInfos;
        }

        public static ObservableCollection<PlayerInfo> GetOnline()
        {
            ObservableCollection<PlayerInfo> playerInfos = new ObservableCollection<PlayerInfo>();

            StreamReader HackerList = new StreamReader(InfoPath.GetHackerListPath());

            while (!HackerList.EndOfStream)
            {
                try
                {
                    PlayerInfo currentPlayer = GetPlayerInfo(FindIdInString(HackerList.ReadLine()));

                    if (currentPlayer.GameStatus != "Offline")
                    {
                        playerInfos.Add(currentPlayer);
                    }
                }
                catch
                {
                    playerInfos.Add(new PlayerInfo("Error", "Error", 0, "Error", new BitmapImage()));
                }
            }

            HackerList.Close();

            return playerInfos;
        }
    }
}
