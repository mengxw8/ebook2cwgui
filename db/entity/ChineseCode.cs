using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CW
{
    [SugarTable("chinese_code")]
    public class ChineseCode
    {
        [SugarColumn(ColumnName = "code")]
        public string? Code { get; set; }

        [SugarColumn(ColumnName = "chinese")]
        public string? Chinese { get; set; }
    }
}
