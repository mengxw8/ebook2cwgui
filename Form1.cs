using System;
using System.Diagnostics;
using System.Reflection;

namespace CW
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //去音频转换小工具
            Convert convert = new Convert();
            this.Visible = false;
            convert.ShowDialog();
            this.Close();
        }

        private void copyBtn_Click(object sender, EventArgs e)
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
            Version version = currentAssembly.GetName().Version;
            this.Text = this.Text + " V" + version;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // 当链接文本被点击时触发的事件
            // 在这里执行你希望的操作，比如打开一个链接或执行一些特定的任务
            System.Diagnostics.Process.Start(new ProcessStartInfo("https://github.com/mengxw8/ebook2cwgui/issues") { UseShellExecute = true });
        }

        private void sendBtn_Click(object sender, EventArgs e)
        {
            SendPractice sendPractice = new SendPractice();
            this.Visible = false;
            sendPractice.ShowDialog();
            this.Close();
        }
    }
}
