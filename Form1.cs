using System.Diagnostics;


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
            ArticleConvert convert = new();

            convert.ShowDialog();

        }

        private void CopyBtn_Click(object sender, EventArgs e)
        {
            CopyingPractice copyingPractice = new();

            copyingPractice.ShowDialog();

        }

        private LinkLabel? updateLink;
        private string? updateUrl;

        private void Form1_Load(object sender, EventArgs e)
        {
            Version version = UpdateChecker.CurrentVersion;
            this.Text = this.Text + " V" + version;
            _ = CheckForUpdateAsync();
        }

        private async Task CheckForUpdateAsync()
        {
            try
            {
                var release = await UpdateChecker.FindNewerReleaseAsync();
                if (release == null || IsDisposed)
                    return;
                ShowUpdateNotice(release);
            }
            catch (Exception)
            {
                // 网络不可用或 GitHub 暂时无法访问时不打扰用户。
            }
        }

        private void ShowUpdateNotice(ReleaseInfo release)
        {
            if (IsDisposed)
                return;

            updateUrl = release.Url;
            if (updateLink == null)
            {
                updateLink = new LinkLabel
                {
                    AutoSize = false,
                    TextAlign = ContentAlignment.MiddleCenter,
                    LinkBehavior = LinkBehavior.HoverUnderline,
                    Size = new Size(ClientSize.Width - 16, 20),
                    Location = new Point(8, ClientSize.Height + 2),
                };
                updateLink.LinkClicked += (_, _) => OpenUpdatePage();
                Controls.Add(updateLink);
                ClientSize = new Size(ClientSize.Width, ClientSize.Height + 28);
                updateLink.Location = new Point(8, ClientSize.Height - 24);
                updateLink.Width = ClientSize.Width - 16;
            }

            updateLink.Text = $"发现新版本 {release.VersionText}，点击下载";
            this.Text = $"CW工具箱 V{UpdateChecker.CurrentVersion}（有更新）";
        }

        private void OpenUpdatePage()
        {
            if (string.IsNullOrWhiteSpace(updateUrl))
                return;
            Process.Start(new ProcessStartInfo(updateUrl) { UseShellExecute = true });
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // 当链接文本被点击时触发的事件
            // 在这里执行你希望的操作，比如打开一个链接或执行一些特定的任务
            System.Diagnostics.Process.Start(new ProcessStartInfo("https://github.com/mengxw8/ebook2cwgui/issues") { UseShellExecute = true });
        }

        private void SendBtn_Click(object sender, EventArgs e)
        {
            SendPractice sendPractice = new();
            sendPractice.ShowDialog();

        }

        private void ShortNumberBtn_Click(object sender, EventArgs e)
        {
            NumberCopyingPractice number = new();

            number.ShowDialog();

        }

        //跳转到中文快查界面
        private void ChineseCodeQuickQueryBtn_Click(object sender, EventArgs e)
        {
            ChineseCodeQuickQuery chineseCodeQuickQuery = new();

            chineseCodeQuickQuery.ShowDialog();


        }

        private void AbbreviationQuickSearchBtn_Click(object sender, EventArgs e)
        {
            AbbreviationQuickSearch abbreviationQuickSearch = new();

            abbreviationQuickSearch.ShowDialog();

        }

        private void ToPlayerBtn_Click(object sender, EventArgs e)
        {
            Player player = new();

            player.ShowDialog();

        }

        private void MultiChannelBtn_Click(object sender, EventArgs e)
        {
            using MultiChannelPlayer player = new();
            player.ShowDialog(this);
        }
    }
}
