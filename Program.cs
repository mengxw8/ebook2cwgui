namespace CW
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            // 子窗口由各功能按钮动态创建，定时同步确保新窗口也继承当前主题。
            using var themeTimer = new System.Windows.Forms.Timer { Interval = 500 };
            themeTimer.Tick += (_, _) =>
            {
                foreach (Form form in Application.OpenForms)
                    ThemeManager.Apply(form);
            };
            themeTimer.Start();
            Application.Run(new Form1());
        }
    }
}
