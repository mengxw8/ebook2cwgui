namespace CW;

internal static class PracticeButtonLayout
{
    public static void UseCompactHeight(params Button[] buttons)
    {
        foreach (var button in buttons)
        {
            button.AutoSize = false;
            button.Padding = Padding.Empty;
            button.FontChanged += (_, _) => FitHeight(button);
            button.DpiChangedAfterParent += (_, _) => FitHeight(button);
            FitHeight(button);
        }
    }

    private static void FitHeight(Button button)
    {
        int textHeight = TextRenderer.MeasureText(
            button.Text, button.Font, Size.Empty,
            TextFormatFlags.SingleLine | TextFormatFlags.NoPadding).Height;
        // 只保留容纳边框及上下留白所需的高度，避免标准自动尺寸额外增高。
        button.Height = textHeight + button.LogicalToDeviceUnits(8);
    }
}
