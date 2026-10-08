using System.Diagnostics;
using System.Text;

namespace CW;

// 捕获时间戳与界面绘制解耦，自动键的结束事件也在这里统一记录。
internal sealed class SendingTimingAnalyzer
{
    private readonly object sync = new();
    private readonly List<(bool Mark, double Ms)> samples = new();
    private long down, up;
    public void Reset() { lock (sync) { samples.Clear(); down = up = 0; } }
    public void Press()
    {
        lock (sync)
        {
            if (down != 0) return;
            long now = Stopwatch.GetTimestamp();
            if (up != 0) samples.Add((false, (now - up) * 1000.0 / Stopwatch.Frequency));
            down = now;
        }
    }
    public void Release()
    {
        lock (sync)
        {
            if (down == 0) return;
            up = Stopwatch.GetTimestamp();
            samples.Add((true, (up - down) * 1000.0 / Stopwatch.Frequency));
            down = 0;
        }
    }
    public string Report(int speed)
    {
        (bool Mark, double Ms)[] data;
        lock (sync) data = samples.ToArray();
        if (data.Length == 0) return "尚无拍发记录。请先操作电键。";
        double unit = 1200.0 / Math.Max(1, speed);
        var dots = data.Where(x => x.Mark && x.Ms < 2 * unit).Select(x => x.Ms).Order().ToArray();
        var dashes = data.Where(x => x.Mark && x.Ms >= 2 * unit).Select(x => x.Ms).ToArray();
        double actual = dots.Length == 0 ? 0 : dots[dots.Length / 2];
        var report = new StringBuilder($"目标速度：{speed} WPM，目标点长：{unit:F1} ms\r\n");
        report.AppendLine(actual > 0 ? $"实际点长中位数：{actual:F1} ms，估计速度：{1200 / actual:F1} WPM" : "缺少点样本，无法估算实际速度。");
        if (dots.Length > 0 && dashes.Length > 0) report.AppendLine($"平均划/点比例：{dashes.Average() / dots.Average():F2}（标准 3）");
        report.AppendLine("判定：偏差≤15% 优秀，≤30% 合格，否则需改进。分类基于目标速度。\n序号\t类别\t实际(ms)\t标准(ms)\t偏差\t评价");
        var code = new StringBuilder(); var decoded = new StringBuilder(); int good = 0;
        void Flush() { if (code.Length > 0) { decoded.Append(Constant.allCode.GetValueOrDefault(code.ToString(), '?')); code.Clear(); } }
        for (int i = 0; i < data.Length; i++)
        {
            var item = data[i];
            int units = item.Mark ? (item.Ms < 2 * unit ? 1 : 3) : (item.Ms < 2 * unit ? 1 : item.Ms < 5 * unit ? 3 : 7);
            string kind = item.Mark ? (units == 1 ? "点" : "划") : (units == 1 ? "符号间隔" : units == 3 ? "字符间隔" : "词间隔");
            double error = (item.Ms / (units * unit) - 1) * 100;
            if (Math.Abs(error) <= 30) good++;
            report.AppendLine($"{i+1}\t{kind}\t{item.Ms:F1}\t{units * unit:F1}\t{error:+0.0;-0.0;0}%\t{(Math.Abs(error) <= 15 ? "优秀" : Math.Abs(error) <= 30 ? "合格" : "需改进")}");
            if (item.Mark) code.Append(units == 1 ? '.' : '-');
            else if (units >= 3) { Flush(); if (units == 7) decoded.Append(' '); }
        }
        Flush();
        report.AppendLine($"\r\n时值合格率：{100.0 * good / data.Length:F1}%\r\n解析报文：{decoded}");
        report.AppendLine("鼠标事件时间包含系统调度延迟；停顿也会计入间隔。清空输入或开始新练习可重新统计。");
        return report.ToString();
    }
}
