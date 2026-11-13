using System.Text;
using System.Text.RegularExpressions;

namespace CW;

internal sealed class PracticeAnswerHighlight
{
    public string Text { get; }
    public List<(int Start, int Length)> Groups { get; } = [];

    public PracticeAnswerHighlight(string answer)
    {
        // 与 RichTextBox 一样归一化换行；保留被隐藏标记占用的播放组序号。
        string source = answer.Replace("\r\n", "\n").Replace('\r', '\n').Replace('q', 'Q');
        var hidden = new bool[source.Length];
        foreach (string marker in new[] { Constant.StartString, Constant.EndString })
        {
            string normalized = marker.Replace("\r\n", "\n").Replace('\r', '\n');
            for (int index = source.IndexOf(normalized, StringComparison.Ordinal); index >= 0;
                index = source.IndexOf(normalized, index + normalized.Length, StringComparison.Ordinal))
                Array.Fill(hidden, true, index, normalized.Length);
        }
        var offsets = new int[source.Length + 1];
        var display = new StringBuilder();
        for (int i = 0; i < source.Length; i++)
        {
            offsets[i] = display.Length;
            if (!hidden[i]) display.Append(source[i]);
        }
        offsets[source.Length] = display.Length;
        Text = display.ToString();
        foreach (Match match in Regex.Matches(source, @"\S+"))
            Groups.Add((offsets[match.Index], offsets[match.Index + match.Length] - offsets[match.Index]));
    }
}
