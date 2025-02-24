using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CW
{
    public class MorseConfig
    {
        public int Di { get; set; }
        public int Da { get; set; }
        public int KeystrokeInterval { get; set; }
        public int CharInterval { get; set; }

        public int Wpm { get; set; }


        public static MorseConfig Speed(int speed) {
           var config= new MorseConfig();

            config.Wpm= speed;
           // 以Paris计
             var di = 60000 / (speed * 50);
            config.Di = di;
            config.Da = di * 3;
            config.KeystrokeInterval = di;
            config.CharInterval = di * 7;
            return config;
        }
    }
}
