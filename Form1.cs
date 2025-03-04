using NAudio.Wave;
using System;
using System.Diagnostics;
using System.Reflection;

using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace CW
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            //去音频转换小工具
            ArticleConvert convert = new ArticleConvert();
            this.Visible = false;
            convert.ShowDialog();
            this.Close();
        }

        private void CopyBtn_Click(object sender, EventArgs e)
        {
            CopyingPractice copyingPractice = new CopyingPractice();
            this.Visible = false;
            copyingPractice.ShowDialog();
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // 获取当前程序集的版本
            Assembly currentAssembly = Assembly.GetExecutingAssembly();
            Version version = currentAssembly.GetName().Version ?? new Version(1, 0, 0, 0);
            this.Text = this.Text + " V" + version;
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // 当链接文本被点击时触发的事件
            // 在这里执行你希望的操作，比如打开一个链接或执行一些特定的任务
            System.Diagnostics.Process.Start(new ProcessStartInfo("https://github.com/mengxw8/ebook2cwgui/issues") { UseShellExecute = true });
        }

        private void SendBtn_Click(object sender, EventArgs e)
        {
            SendPractice sendPractice = new SendPractice();
            this.Visible = false;
            sendPractice.ShowDialog();
            this.Close();
        }

        private void ShortNumberBtn_Click(object sender, EventArgs e)
        {
            NumberCopyingPractice number = new NumberCopyingPractice();
            this.Visible = false;
            number.ShowDialog();
            this.Close();
        }

        //跳转到中文快查界面
        private void chineseCodeQuickQueryBtn_Click(object sender, EventArgs e)
        {
            ChineseCodeQuickQuery chineseCodeQuickQuery=new  ChineseCodeQuickQuery();
                this.Visible = false;
            chineseCodeQuickQuery.ShowDialog();
            this.Close();

        }
    }
}
