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
            using var inputLanguageScope = new InputLanguageScope();
            ApplicationConfiguration.Initialize();
            // 新窗口首次进入消息循环时应用一次主题；主题切换由 ThemeManager.Set 统一刷新。
            Application.Idle += (_, _) => ThemeManager.ApplyNewForms();
            Application.Run(new Form1());
        }
    }
}
