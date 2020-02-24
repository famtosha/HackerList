using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace FirstWpf
{
    class PlayerInfo
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public int VAC { get; set; }
        public string GameStatus { get; set; }
        public BitmapImage Avatar { get; set; }

        public PlayerInfo(string Name, string ID, int VAC, string GameStatus, BitmapImage Avatar)
        {
            this.Name = Name;
            this.ID = ID;
            this.VAC = VAC;
            this.GameStatus = GameStatus;
            this.Avatar = Avatar;
        }

        public PlayerInfo()
        {

        }
    }
}
