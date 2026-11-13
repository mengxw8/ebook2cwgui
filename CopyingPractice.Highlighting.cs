using System.Text.RegularExpressions;
using NAudio.Wave;

namespace CW;

public partial class CopyingPractice
{
    private readonly System.Windows.Forms.Timer answerHighlightTimer = new() { Interval = 100 };
    private PracticeAnswerHighlight? playbackHighlight;
    private Action<string>? groupPlaybackHandler;
    private int answerPlaybackGeneration;
    private int playedAnswerGroup = -1;
    private int answerHeaderGroups;
    private int highlightedAnswerStart = -1;
    private int highlightedAnswerLength;
    private bool trackingAnswerPlayback;

    private void InitializeAnswerHighlighting()
    {
        components.Add(answerHighlightTimer);
        answerHighlightTimer.Tick += (_, _) =>
        {
            // 播放器在正文结束后继续输出静音；空报尾时也要清掉最后一组。
            if (trackingAnswerPlayback && playedAnswerGroup >= 0 &&
                playerWave.PlaybackState == PlaybackState.Playing && !player.HasQueuedAudio)
                StopAnswerTracking();
        };
    }

    private void BeginAnswerTracking()
    {
        StopAnswerTracking();
        playbackHighlight = new PracticeAnswerHighlight(answer);
        answerHeaderGroups = Regex.Matches(msgStartTxb.Text, @"\S+").Count;
        playedAnswerGroup = -1;
        trackingAnswerPlayback = true;
        int generation = answerPlaybackGeneration;
        groupPlaybackHandler = _ =>
        {
            if (IsDisposed || Disposing || !IsHandleCreated) return;
            try
            {
                BeginInvoke((Action)(() =>
                {
                    if (generation != answerPlaybackGeneration || !trackingAnswerPlayback || IsDisposed || Disposing) return;
                    playedAnswerGroup++;
                    UpdateAnswerHighlight();
                }));
            }
            catch (InvalidOperationException) when (IsDisposed || Disposing || !IsHandleCreated)
            {
                // 窗口关闭后丢弃剩余的音频线程通知。
            }
        };
        player.OnGroupPlay += groupPlaybackHandler;
        answerHighlightTimer.Start();
    }

    private void StopAnswerTracking()
    {
        answerPlaybackGeneration++;
        trackingAnswerPlayback = false;
        answerHighlightTimer.Stop();
        if (groupPlaybackHandler != null)
        {
            player.OnGroupPlay -= groupPlaybackHandler;
            groupPlaybackHandler = null;
        }
        ResetAnswerHighlight();
    }

    private void ResetAnswerHighlight()
    {
        if (highlightedAnswerStart >= 0 && highlightedAnswerStart + highlightedAnswerLength <= answerBox.TextLength)
            FormatAnswerRange(highlightedAnswerStart, highlightedAnswerLength, answerBox.BackColor, answerBox.ForeColor);
        highlightedAnswerStart = -1;
        highlightedAnswerLength = 0;
    }

    private void UpdateAnswerHighlight()
    {
        ResetAnswerHighlight();
        int bodyIndex = playedAnswerGroup - answerHeaderGroups;
        if (!trackingAnswerPlayback || !showAnswerChb.Checked || playbackHighlight == null ||
            answerBox.Text != playbackHighlight.Text || bodyIndex < 0 || bodyIndex >= playbackHighlight.Groups.Count)
            return;
        var range = playbackHighlight.Groups[bodyIndex];
        if (range.Length == 0) return;
        FormatAnswerRange(range.Start, range.Length, Color.LightYellow, Color.Black);
        highlightedAnswerStart = range.Start;
        highlightedAnswerLength = range.Length;
    }

    private void FormatAnswerRange(int start, int length, Color back, Color fore)
    {
        int selectionStart = answerBox.SelectionStart;
        int selectionLength = answerBox.SelectionLength;
        answerBox.Select(start, length);
        answerBox.SelectionBackColor = back;
        answerBox.SelectionColor = fore;
        answerBox.Select(selectionStart, selectionLength);
        // 不让后续手动输入继承播放高亮。
        if (selectionLength == 0)
        {
            answerBox.SelectionBackColor = answerBox.BackColor;
            answerBox.SelectionColor = answerBox.ForeColor;
        }
    }
}
