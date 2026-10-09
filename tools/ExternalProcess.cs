using System.Diagnostics;

namespace CW;

internal static class ExternalProcess
{
    internal static async Task<(string Output, int ExitCode)> RunAsync(ProcessStartInfo startInfo, TimeSpan? timeout = null)
    {
        using var process = Process.Start(startInfo) ?? throw new InvalidOperationException("无法启动转换程序。");
        using var cancellation = new CancellationTokenSource(timeout ?? TimeSpan.FromMinutes(10));
        // 启动读取后再等待退出，持续排空管道；等待和读取都受同一个超时限制。
        Task<string> output = process.StandardOutput.ReadToEndAsync(cancellation.Token);
        try
        {
            await Task.WhenAll(output, process.WaitForExitAsync(cancellation.Token)).ConfigureAwait(false);
            return (await output.ConfigureAwait(false), process.ExitCode);
        }
        catch (OperationCanceledException)
        {
            if (!process.HasExited)
                process.Kill(entireProcessTree: true);
            try { await output.ConfigureAwait(false); }
            catch (OperationCanceledException) { }
            throw new TimeoutException("转换超时，已终止转换程序。请减少报文长度后重试。");
        }
    }
}
