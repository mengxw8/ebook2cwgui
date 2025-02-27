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
        public int WordInterval { get; set; }

        public int Speed { get; set; }


        public static MorseConfig Create(int speed)
        {
            var config = new MorseConfig()
            {
                Speed = speed,
            };

            // 以Paris计
            var di = 1200 / speed ;
            config.Di = di;
            config.Da = di * 3;
            config.KeystrokeInterval = di;
            config.CharInterval = di * 3;
            config.WordInterval = di * 7;
            return config;
        }
    }
}
