using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CW
{
    public class MorseConfig
    {
        /// <summary>
        /// Di的长度 ms
        /// </summary>
        public int Di { get; set; }
        /// <summary>
        /// Da的长度 ms
        /// </summary>
        public int Da { get; set; }
        /// <summary>
        /// 键击间隔 ms = Di 
        /// </summary>
        public int KeystrokeInterval { get; set; }
        /// <summary>
        /// 字符间隔 ms =3*Di
        /// 
        /// </summary>
        public int CharInterval { get; set; }
        /// <summary>
        /// 词间隔 ms =7*Di
        /// </summary>
        public int WordInterval { get; set; }
        /// <summary>
        /// 速度WPM
        /// </summary>
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
