using System.Drawing;

namespace CW;

/// <summary>统一管理深色和高对比度配色，并保存用户选择。</summary>
internal enum AppTheme { Light, Dark, HighContrast, System }

internal static class ThemeManager
{
    private static readonly string SettingsPath = Path.Combine(AppContext.BaseDirectory, "cw-theme.txt");
    public static AppTheme Current { get; private set; } = Load();

    public static void Apply(Control root)
    {
        var (back, fore, input, accent) = Current switch
        {
            AppTheme.Dark => (Color.FromArgb(32, 32, 32), Color.Gainsboro, Color.FromArgb(48, 48, 48), Color.FromArgb(0, 122, 204)),
            AppTheme.HighContrast => (Color.Black, Color.White, Color.Black, Color.Yellow),
            AppTheme.System => (SystemColors.Control, SystemColors.ControlText, SystemColors.Window, SystemColors.Highlight),
            _ => (SystemColors.Control, SystemColors.ControlText, SystemColors.Window, SystemColors.Highlight)
        };
        ApplyControl(root, back, fore, input, accent);
    }

    private static readonly HashSet<Form> appliedForms = new();

    public static void ApplyNewForms()
    {
        foreach (Form form in Application.OpenForms)
        {
            if (appliedForms.Add(form))
                Apply(form);
        }
    }

    public static void Set(AppTheme theme)
    {
        Current = theme;
        try { File.WriteAllText(SettingsPath, theme.ToString()); }
        catch (Exception ex) { System.Diagnostics.Trace.WriteLine($"主题设置保存失败: {ex}"); }
        appliedForms.Clear();
        ApplyNewForms();
    }

    private static AppTheme Load()
    {
        try { return Enum.TryParse<AppTheme>(File.ReadAllText(SettingsPath), true, out var value) ? value : AppTheme.Light; }
        catch { return AppTheme.Light; }
    }

    private static void ApplyControl(Control control, Color back, Color fore, Color input, Color accent)
    {
        control.BackColor = back;
        control.ForeColor = fore;
        if (control is TextBoxBase or NumericUpDown or ComboBox or ListControl)
            control.BackColor = input;
        if (control is Button button)
        {
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderColor = accent;
        }
        foreach (Control child in control.Controls)
            ApplyControl(child, back, fore, input, accent);
    }
}
