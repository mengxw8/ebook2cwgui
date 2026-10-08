namespace CW;

// 在同一个 UI 线程上保存和恢复输入法，嵌套窗口各自恢复打开前的状态。
internal sealed class InputLanguageScope : IDisposable
{
    private readonly InputLanguage original = InputLanguage.CurrentInputLanguage;
    private bool restored;

    public void Dispose()
    {
        if (restored)
            return;

        InputLanguage.CurrentInputLanguage = original;
        restored = true;
    }
}
