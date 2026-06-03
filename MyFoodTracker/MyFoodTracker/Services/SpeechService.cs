namespace MyFoodTracker.Services;

public static class SpeechService
{
    private static CancellationTokenSource? _cts;
    public static async Task SpeakAsync(string text)
    {
        Stop();
        _cts = new CancellationTokenSource();
        try
        {
            await TextToSpeech.Default.SpeakAsync(text, cancelToken: _cts.Token);
        }
        catch (OperationCanceledException) { }
    }
    public static void Stop()
    {
        _cts?.Cancel();
        _cts?.Dispose();
        _cts = null;
    }
}