using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CW.db.entity
{
    [SugarTable("abbreviations")]
    internal class Abbreviations
    {
        /// <summary>
        /// 简语
        /// </summary>
        [DisplayName("简语")]
        [SugarColumn(ColumnName = "word")]
        public string? Word { get; set; }
        /// <summary>
        /// 原词
        /// </summary>
        [DisplayName("原词")]
        [SugarColumn(ColumnName = "sense")]
        public string? Sense { get; set; }
        /// <summary>
        /// 大致意思
        /// </summary>
        [DisplayName("含义")]
        [SugarColumn(ColumnName = "explanation")]
        public string? Explanation { get; set; }
        /// <summary>
        /// 释义
        /// </summary>
        [DisplayName("不同语境释义")]
        [SugarColumn(ColumnName = "purpose")]
        public string? Purpose { get; set; }
        /// <summary>
        /// 是否常用，0不常用
        /// </summary>
        [DisplayName("是否常用，1常用")]
        [SugarColumn(ColumnName = "usual")]
        public int? Usual { get; set; }

    }
}
