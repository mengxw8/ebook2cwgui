using System;

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
            CopyingPractice copyingPractice=new CopyingPractice();
            this.Visible = false;
            copyingPractice.ShowDialog();
            this.Close();
        }
    }
}
