using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;

namespace FirstWpf
{
    class InfoPath
    {
        public static void IsExist()
        {
            if (!File.Exists(Properties.Settings.Default.HackerListPath) && !File.Exists(Properties.Settings.Default.HackerInfoPath))
            {
                throw new Exception("Cannot find path");
            }
        }
        public static void SetHackerListPath(string Path)
        {
            if (File.Exists(Path))
            {
                Properties.Settings.Default.HackerListPath = Path;
            }
            else
            {
                throw new Exception("Cannot find path");
            }
        }
        public static void SetHackerInfoPath(string Path)
        {
            if (File.Exists(Path))
            {
                Properties.Settings.Default.HackerInfoPath = Path;
            }
            else
            {
                throw new Exception("Cannot find path");
            }
        }

        public static string GetHackerListPath()
        {
            return Properties.Settings.Default.HackerListPath;
        }

        public static string GetHackerInfoPath()
        {
            return Properties.Settings.Default.HackerInfoPath;
        }
    }
}
