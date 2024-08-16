using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CW
{
    public partial class Convert : Form
    {

        private const string configFileName= "cwConfig.ini"; 
        public Convert()
        {
            InitializeComponent();
        }
        [DllImport("kernel32.dll")] //写INI
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32.dll")] //读INI
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        /// <summary>
        /// 读取INI文件值
        /// </summary>
        /// <param name="section">节点名</param>
        /// <param name="key">键</param>
        /// <param name="def">未取到值时返回的默认值</param>
        /// <param name="filePath">INI文件完整路径</param>
        /// <returns>读取的值</returns>
        public string Read(string section, string key, string def, string filePath)
        {
            StringBuilder sb = new StringBuilder(1024);
            GetPrivateProfileString(section, key, def, sb, 1024, filePath);
            return sb.ToString();
        }
        /// <summary>
        /// 写INI文件值
        /// </summary>
        /// <param name="section">欲在其中写入的节点名称</param>
        /// <param name="key">欲设置的项名</param>
        /// <param name="value">要写入的新字符串</param>
        /// <param name="filePath">INI文件完整路径</param>
        /// <returns>非零表示成功，零表示失败</returns>
        public long Write(string section, string key, string value, string filePath)
        {

            return WritePrivateProfileString(section, key, value, filePath);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            // 当链接文本被点击时触发的事件
            // 在这里执行你希望的操作，比如打开一个链接或执行一些特定的任务
            System.Diagnostics.Process.Start(new ProcessStartInfo("https://fkurz.net/ham/ebook2cw.html") { UseShellExecute = true });

        }

        private void inputFilePathBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;//该值确定是否可以选择多个文件
            dialog.Title = "请选择文件";
            dialog.Filter = "文本文件(*.txt)|*.txt";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                inputFilePathTxb.Text = dialog.FileName;

            }
        }

        private void outputFilePathBtn_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择输出文件夹";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                outputFilePathTxb.Text = dialog.SelectedPath;


            }
        }

        private void ConvertBtn_Click(object sender, EventArgs e)
        {
            //校验数据
            //校验输入文件
            var inputFile = inputFilePathTxb.Text;
            if (!File.Exists(inputFile))
            {
                MessageBox.Show("待转换文件不正确,请重新选择！");
                return;
            }
            //校验输出目录
            var outPath = outputFilePathTxb.Text;
            if (!Directory.Exists(outPath))
            {
                //创建文件夹
                try
                {
                    Directory.CreateDirectory(outPath);
                }
                catch (Exception)
                {
                    MessageBox.Show("文件输出路径不正确,请重新选择！");
                    return;
                }
            }

     
            var args = new Dictionary<string, string>();

            args.Add("q","1");


            //分隔符
            var separator = separatorTxb.Text;
            if (separator != "")
            {
                args.Add("c" , separator);
            }
            //速度
            var speed = speedTxb.Value;
            args.Add("w" , speed.ToString());
            //eff. Speed
            var effSpeed=effSpeedTxb.Value;
            if (effSpeed != 0) {
                args.Add("e", effSpeed.ToString());
            }
            //额外字间距
           var extraSpace= extraSpaceTxb.Value;
            if (extraSpace != 0) {
                args.Add("W" , extraSpace.ToString());
            }


            //频率
            var tone=toneTxb.Value;
            args.Add("f" , tone.ToString());
            //波形
          var waveformType=   waveform.SelectedIndex;
            args.Add("T" , waveformType.ToString());


            //输出信息
            var fileName= fileNameTxb.Text;
            if (fileName != "") {
                args.Add("o" , outPath+"\\"+fileName);
            }

            //作者

           var author=   authorTxb.Text;
            if (author != "")
            {
                args.Add("a" , fileName);
            }
            var title = titleTxb.Text;
            if (title != "")
            {
                args.Add("t" , title);
            }
            else {
                args.Add("t" ,fileName);
            }

            var comment = commentTxb.Text;
            if (comment != "")
            {
                args.Add("k" , comment);
            }

           var year= dateTxb.Text;
            if (year != "")
            {
                args.Add("y" , year);
            }
            //输出文件类型
            var fileFormatType=fileFormat.SelectedIndex;
            if (fileFormatType != 0) {
                args.Add("O",null);
            }


            var wordsLimit= wordsLimitTxb.Value;
            if (wordsLimit > 0) {
                args.Add("l", wordsLimit.ToString());
            }

            var timeLimit = timeLimitTxb.Value;
            if (timeLimit > 0)
            {
                args.Add("d", timeLimit.ToString());
            }
            //禁止重新设置速度的时候使用 - Q选项
            if (!resetSpeedCbx.Checked)
            {
                args.Add("n",null);
            }
            else {
                var qrqValue = QRQ.Value;
                if (qrqValue != 0)
                {
                    args.Add("Q" , qrqValue.ToString());
                }
            }
            



            if (disableBtCbx.Checked)
            {
                args.Add("p",null);
            }
            //其余参数
            var parameters= parametersTxb.Text;
            if (parameters != "") {
                args.Add(parameters,null);
            }

            //构建命令
            var param = "";
            // 遍历字典
            foreach (var pair in args)
            {
                param += "-" + pair.Key + " ";
                if (pair.Value != null) {
                    param += pair.Value + " ";
                }
            }

            param += inputFile;
            Console.WriteLine(param);
 
            ProcessStartInfo startInfo = new ProcessStartInfo("ebook2cw.exe", param);

            startInfo.UseShellExecute = false;    //是否使用操作系统的shell启动
            //startInfo.RedirectStandardInput = true;      //接受来自调用程序的输入     
            startInfo.RedirectStandardOutput = true;     //由调用程序获取输出信息
            startInfo.CreateNoWindow = true;             //不显示调用程序的窗口 
                                                         //创建进程对象   


            try
            {
                //调用EXE
                using var process = Process.Start(startInfo);
                
                using var reader = process.StandardOutput;
                // 获取exe的输出结果
                string result = reader.ReadToEnd(); 
                if (result != "")
                {
                    string[] lines = result.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                    if (lines.Length >= 2)
                    {
                        int startIndex = lines.Length - 3;
                        result = string.Join(Environment.NewLine, lines.Skip(startIndex));
                    }
                    
                    if (result.IndexOf("Error:") >= 0)
                    {
                        MessageBox.Show("配置错误，转换失败，请检查！");
                    }
                    else
                    {
                        var data = lines[lines.Length - 3].Split(":");
                        if (data.Length == 3)
                        {
                            MessageBox.Show("转换完成，共计用时" + data[2] + "！");
                            //保存配置文件
                            saveConfig(args);
                        }
                    }

                }
            }
            catch (Exception ) {
                MessageBox.Show("程序错误，请重新下载！");
            }


        }

        private void saveConfig(Dictionary<string, string> configs) {
            //写配置
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, configFileName);
            foreach (var pair in configs)
            {
                if (pair.Value != null) {
                    Write("settings", pair.Key, pair.Value, filePath);
                }
            }         
        }


        private void Convert_Load(object sender, EventArgs e)
        {

            //TODO 初始化配置文件
            //初始化控件
            speedTxb.Minimum = 0;
            effSpeedTxb.Minimum = 0;
            extraSpaceTxb.Minimum = 0;
            QRQ.Minimum = 0;
            toneTxb.Minimum = 0;
            toneTxb.Maximum = 99999;
            //默认选中第一个
            waveform.SelectedIndex = 0;
            fileFormat.SelectedIndex=0;
            dateTxb.Text = DateTime.Now.Year.ToString();
            // 获取当前Windows身份
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            // 从身份中获取用户名
            authorTxb.Text= identity.Name;

            //加载配置文件
            loadConfig();

        }

        private void loadConfig() {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, configFileName);
            if (File.Exists(filePath))
            {
                var outPath=Read("settings", "o", "", filePath);

                //文件存在则恢复配置
                if (outPath != "") {
                    outputFilePathTxb.Text = outPath.Substring(0, outPath.LastIndexOf("\\"));
                }  
                fileNameTxb.Text = outPath.Substring(outPath.LastIndexOf("\\")+1);
                separatorTxb.Text = Read("settings", "c", "", filePath);

                speedTxb.Value = System.Convert.ToInt32( Read("settings", "w", "20", filePath)) ; 
                effSpeedTxb.Value = System.Convert.ToInt32( Read("settings", "e", "0", filePath)) ; 
                extraSpaceTxb.Value = System.Convert.ToInt32( Read("settings", "W", "0", filePath)) ; 
                QRQ.Value = System.Convert.ToInt32( Read("settings", "Q", "0", filePath)) ; 
                toneTxb.Value = System.Convert.ToInt32( Read("settings", "f", "600", filePath)) ; 
                waveform.SelectedIndex= System.Convert.ToInt32(Read("settings", "T", "0", filePath));
                authorTxb.Text = Read("settings", "a", "", filePath);
                titleTxb.Text = Read("settings", "t", "", filePath);
                commentTxb.Text = Read("settings", "k", "", filePath);
                dateTxb.Text = Read("settings", "y", "", filePath);
                wordsLimitTxb.Value= System.Convert.ToInt32(Read("settings", "l", "0", filePath));
                timeLimitTxb.Value= System.Convert.ToInt32(Read("settings", "d", "0", filePath));




            }
            else
            {
                File.Create(filePath);
            }

        }

        private void speedTxb_Leave(object sender, EventArgs e)
        {
            if (speedTxb.Value < 1) {
                speedTxb.Value = 1;
            }
        }
    }
}
