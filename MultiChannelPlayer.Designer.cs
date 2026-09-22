using System.ComponentModel;

namespace CW
{
    partial class MultiChannelPlayer
    {
        private void InitializeComponent()
        {
            SuspendLayout();
            Text = "多路播放";
            StartPosition = FormStartPosition.CenterParent;
            ClientSize = new Size(1440, 900);
            MinimumSize = new Size(1280, 800);
            AutoScaleMode = AutoScaleMode.Font;
            Name = "MultiChannelPlayer";
            BuildLayout();
            ResumeLayout(false);
        }
    }
}

