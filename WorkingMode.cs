using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CW
{
    enum WorkingMode:int
    {
        None = -1,
        Number = 0,
        Alphabet=1,
        AlphabetAndNumber=2,
        Symbol=3,
        Article=4,
        News=5,
        Word=6,
        Customize=7,
        Koch=8,
        //短5
        ShortNumber5=9,
        //短10
        ShortNumber10 = 10,
    }
}
