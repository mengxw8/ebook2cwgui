using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CW
{
    [SugarTable("word")]
    public class Words
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnName = "id")]//数据库是自增才配自增 
        public int Id { get; set; }
        [SugarColumn(ColumnName = "words")]
        public string? Word { get; set; }
        [SugarColumn(ColumnName = "a")]
        public int A { get; set; }
        [SugarColumn(ColumnName = "b")]
        public int B { get; set; }
        [SugarColumn(ColumnName = "c")]
        public int C { get; set; }
        [SugarColumn(ColumnName = "d")]
        public int D { get; set; }
        [SugarColumn(ColumnName = "e")]
        public int E { get; set; }
        [SugarColumn(ColumnName = "f")]
        public int F { get; set; }
        [SugarColumn(ColumnName = "g")]
        public int G { get; set; }
        [SugarColumn(ColumnName = "h")]
        public int H { get; set; }
        [SugarColumn(ColumnName = "i")]
        public int I { get; set; }
        [SugarColumn(ColumnName = "j")]
        public int J { get; set; }
        [SugarColumn(ColumnName = "k")]
        public int K { get; set; }
        [SugarColumn(ColumnName = "l")]
        public int L { get; set; }
        [SugarColumn(ColumnName = "m")]
        public int M { get; set; }
        [SugarColumn(ColumnName = "n")]
        public int N { get; set; }
        [SugarColumn(ColumnName = "o")]
        public int O { get; set; }
        [SugarColumn(ColumnName = "p")]
        public int P { get; set; }
        [SugarColumn(ColumnName = "q")]
        public int Q { get; set; }
        [SugarColumn(ColumnName = "r")]
        public int R { get; set; }
        [SugarColumn(ColumnName = "s")]
        public int S { get; set; }
        [SugarColumn(ColumnName = "t")]
        public int T { get; set; }
        [SugarColumn(ColumnName = "u")]
        public int U { get; set; }
        [SugarColumn(ColumnName = "v")]
        public int V { get; set; }
        [SugarColumn(ColumnName = "w")]
        public int W { get; set; }
        [SugarColumn(ColumnName = "x")]
        public int X { get; set; }
        [SugarColumn(ColumnName = "y")]
        public int Y { get; set; }
        [SugarColumn(ColumnName = "z")]
        public int Z { get; set; }
    }
}
