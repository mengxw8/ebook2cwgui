
using CW.db.entity;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CW
{
    public partial class AbbreviationQuickSearch : Form
    {

        private HashSet<string> suggestions = new(); // 存放候选项的列表
        private readonly SqlSugarClient db = SqliteUtil.CreateClient();
        public AbbreviationQuickSearch()
        {
            InitializeComponent();
            //查询条件的自动补全
            // 添加一些候选项到suggestions列表中


            queryBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend; // 设置为自动追加模式
            queryBox.AutoCompleteSource = AutoCompleteSource.CustomSource; // 设置为自定义源
            queryBox.AutoCompleteCustomSource.AddRange(suggestions.ToArray()); // 设置自定义源为suggestions列表

            //隐藏表头
            //historyTable.ColumnHeadersVisible = false;
            historyTable.RowHeadersVisible = false;
            //不允许拖动边界更改大小
            historyTable.AllowUserToResizeColumns = false;
            historyTable.AllowUserToResizeRows = false;
            //不要选中的单元格颜色
            historyTable.DefaultCellStyle.SelectionBackColor = Color.White;

            historyTable.DefaultCellStyle.SelectionForeColor = Color.Black;
            //单元格内容居中
            historyTable.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


            //不显示表格线
            //historyTable.CellBorderStyle = DataGridViewCellBorderStyle.None;

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {

            //回车的时候判断有没有合适的字，有的话就直接显示
            if (e.KeyCode == Keys.Enter)
            {
                var queryStr = queryBox.Text;


                var list = db.Queryable<Abbreviations>().Where(it => it.Word == queryStr.ToUpper()).ToList();
      
                //显示当前查询的字
                //ChineseLab.Text = chinese.Chinese;
                //codeLab.Text = chinese.Code;
                //记录进历史记录
                addHistory(list);
                //移除输入
                queryBox.Text = "";
                queryBox.Focus();
                //


            }
            else
            {
                //智能提示

                //var exp = Expressionable.Create<ChineseCode>();
                //exp.OrIF(isNumber, it => it.Code.Contains((char)(e.KeyCode)));//拼接OR
                //exp.OrIF(queryBox.Text.Length==0 && isChinese, it => it.Chinese == Convert.ToString((char)(e.KeyCode)));//拼接OR

                //suggestions.Clear();
                //db.Queryable<ChineseCode>().Where(exp.ToExpression()).Take(10).ToList().ForEach(c => { suggestions.Add(c.Chinese);suggestions.Add(c.Code); });
                //queryBox.AutoCompleteCustomSource.AddRange([.. suggestions]);

            }

        }
        //添加进历史记录
        private void addHistory(List<Abbreviations> abbreviationsList)
        {
            historyTable.DataSource = abbreviationsList;
            historyTable.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
        }

        private void cleanBtn_Click(object sender, EventArgs e)
        {
            historyTable.DataSource = null;
            ChineseLab.Text = "";
            codeLab.Text = "";
            queryBox.Text = "";
            queryBox.Focus();

        }
    }
}
