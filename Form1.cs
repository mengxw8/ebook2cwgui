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
            //ȥ��Ƶת��С����
            Convert convert = new Convert();
            this.Visible = false;
            convert.ShowDialog();
            this.Close();
        }
    }
}
