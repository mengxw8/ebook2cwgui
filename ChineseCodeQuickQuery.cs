
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
    public partial class ChineseCodeQuickQuery : Form
    {

        private readonly HashSet<string> suggestions = []; // 存放候选项的列表
        private readonly SqlSugarClient db = SqliteUtil.CreateClient();
        private readonly DataTable dataTable = new();
        private readonly static int maxColumn = 12;



        Dictionary<string, string> chineseCode;
        Dictionary<string, string> codeChinese;

        private int startIndex = 0;
        public ChineseCodeQuickQuery()
        {
            InitializeComponent();
            //查询条件的自动补全
            // 添加一些候选项到suggestions列表中


            queryBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend; // 设置为自动追加模式
            queryBox.AutoCompleteSource = AutoCompleteSource.CustomSource; // 设置为自定义源
            queryBox.AutoCompleteCustomSource.AddRange([.. suggestions]); // 设置自定义源为suggestions列表

            //隐藏表头
            historyTable.ColumnHeadersVisible = false;
            historyTable.RowHeadersVisible = false;
            //不允许拖动边界更改大小
            historyTable.AllowUserToResizeColumns = false;
            historyTable.AllowUserToResizeRows = false;
            //不要选中的单元格颜色
            historyTable.DefaultCellStyle.SelectionBackColor = Color.White;

            historyTable.DefaultCellStyle.SelectionForeColor = Color.Black;
            //单元格内容居中
            historyTable.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            //绘制空表格
            for (int i = 0; i < maxColumn; i++)
            {
                dataTable.Columns.Add(i.ToString(), typeof(string));
                dataTable.Rows.Add(dataTable.NewRow());
                dataTable.Rows.Add(dataTable.NewRow());

            }
            historyTable.DataSource = dataTable;
            //不显示表格线
            historyTable.CellBorderStyle = DataGridViewCellBorderStyle.None;

        }

        //private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        //{
        //    // 判断是否为数字键或小键盘数字键
        //    bool isNumber = (e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9) ||
        //                    (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9);
        //    // 使用正则表达式匹配汉字
        //    bool isChinese = ((char)e.KeyCode >= '\u4e00' && (char)e.KeyCode <= '\u9fa5');
        //    //回车的时候判断有没有合适的字，有的话就直接显示
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        var queryStr = queryBox.Text;

        //        var exp = Expressionable.Create<ChineseCode>();
        //        exp.Or(it => it.Code == queryStr);//拼接OR
        //        exp.OrIF(queryBox.Text.Length == 1, it => it.Chinese == queryStr);//拼接OR
        //        var list = db.Queryable<ChineseCode>().Where(exp.ToExpression()).ToList();
        //        if (list.Count != 1)
        //        {
        //            return;
        //        }
        //        var chinese = list[0];
        //        //显示当前查询的字


        //        //记录进历史记录
        //        AddHistory(chinese);
        //        //移除输入
        //        queryBox.Text = "";
        //        queryBox.Focus();
        //        //


        //    }
        //    else
        //    {
        //        //智能提示

        //        //var exp = Expressionable.Create<ChineseCode>();
        //        //exp.OrIF(isNumber, it => it.Code.Contains((char)(e.KeyCode)));//拼接OR
        //        //exp.OrIF(queryBox.Text.Length==0 && isChinese, it => it.Chinese == Convert.ToString((char)(e.KeyCode)));//拼接OR

        //        //suggestions.Clear();
        //        //db.Queryable<ChineseCode>().Where(exp.ToExpression()).Take(10).ToList().ForEach(c => { suggestions.Add(c.Chinese);suggestions.Add(c.Code); });
        //        //queryBox.AutoCompleteCustomSource.AddRange([.. suggestions]);

        //    }

        //}
        //添加进历史记录
        private void AddHistory(ChineseCode chinese)
        {
            int rowIndex = (startIndex / maxColumn) * 2;
            int columnIndex = startIndex % maxColumn;
            historyTable.Rows[rowIndex].Cells[columnIndex].Value = chinese.Chinese;
            historyTable.Rows[rowIndex + 1].Cells[columnIndex].Value = chinese.Code;
            ++startIndex;


        }

        private void CleanBtn_Click(object sender, EventArgs e)
        {
            dataTable.Clear();
            startIndex = 0;
            for (int i = 0; i < maxColumn; i++)
            {

                dataTable.Rows.Add(dataTable.NewRow());
                dataTable.Rows.Add(dataTable.NewRow());

            }


            queryBox.Text = "";
            queryBox.Focus();

        }

        private void toChineseBtn_Click(object sender, EventArgs e)
        {
            if (queryBox.Text == "")
            {
                MessageBox.Show("查询内容不能为空,请检查输入内容！", "转换错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                queryBox.Focus();
                return;
            }
            string[] data = queryBox.Text.Split(' ');
            foreach (var item in data)
            {
             
                if (codeChinese.TryGetValue(item,out string chinese)) {
                    AddHistory(new ChineseCode()
                    {
                        Chinese = chinese,
                        Code = item
                    });
                }

            }

        }

        private void toCodeBtn_Click(object sender, EventArgs e)
        {
            if (queryBox.Text == "")
            {
                MessageBox.Show("查询内容不能为空,请检查输入内容！", "转换错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                queryBox.Focus();
                return;
            }
            foreach (var item in queryBox.Text)
            {
            
                if (chineseCode.TryGetValue(item.ToString(),out string code)) {
                    AddHistory(new ChineseCode()
                    {
                        Chinese = item.ToString(),
                        Code = code
                    });
                }
    

            }
        }

        private void ChineseCodeQuickQuery_Load(object sender, EventArgs e)
        {
           var codeList=  db.Queryable<ChineseCode>().ToList();
           chineseCode= codeList.Where(item => item.Code != null && item.Chinese!=null).ToDictionary(
            item => item.Chinese,   // 指定哪个属性作为 Key
            item => item.Code  // 指定哪个属性作为 Value
        );

            codeChinese = codeList.Where(item => item.Code != null && item.Chinese != null).ToDictionary(
 item => item.Code,   // 指定哪个属性作为 Key
 item => item.Chinese  // 指定哪个属性作为 Value
);
        }
    }
}
