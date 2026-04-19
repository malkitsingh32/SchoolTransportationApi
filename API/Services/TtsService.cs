using System.Speech.Synthesis;

namespace TTS_API.Services;

public class TtsService : ITtsService
{
    private readonly object _lock = new();

    public byte[] Synthesize(string text, string? voice = null, int? rate = null, int? volume = null)
    {
        byte[] audioData = null;

        // Create a dedicated STA thread for SpeechSynthesizer
        var thread = new Thread(() =>
        {
            // SpeechSynthesizer is not thread-safe
            using var synthesizer = new SpeechSynthesizer();

            // Select voice if provided
            if (!string.IsNullOrEmpty(voice))
            {
                try
                {
                    synthesizer.SelectVoice(voice);
                }
                catch (ArgumentException)
                {
                    throw new ArgumentException($"Voice '{voice}' not found");
                }
            }

            // Set rate & volume safely
            if (rate.HasValue)
                synthesizer.Rate = Math.Clamp(rate.Value, -10, 10);

            if (volume.HasValue)
                synthesizer.Volume = Math.Clamp(volume.Value, 0, 100);

            using var stream = new MemoryStream();
            synthesizer.SetOutputToWaveStream(stream);

            // Wrap text with leading pause to avoid truncation
            var ssml = WrapWithLeadingPause(text);
            synthesizer.SpeakSsml(ssml);

            audioData = stream.ToArray();
        });

        // Set thread to STA
        thread.SetApartmentState(ApartmentState.STA);

        // Start and wait for synthesis to complete
        thread.Start();
        thread.Join();

        return audioData;
    }

    public IEnumerable<VoiceInfo> GetVoices()
    {
        using var synthesizer = new SpeechSynthesizer();
        
        return synthesizer.GetInstalledVoices()
            .Where(v => v.Enabled)
            .Select(v => new VoiceInfo(
                v.VoiceInfo.Name,
                v.VoiceInfo.Gender.ToString(),
                v.VoiceInfo.Age.ToString(),
                v.VoiceInfo.Culture.Name
            ))
            .ToList();
    }

    private static string WrapWithLeadingPause(string text, int pauseMs = 200)
    {
        var escapedText = text
            .Replace("&", "&amp;")
            .Replace("<", "&lt;")
            .Replace(">", "&gt;")
            .Replace("\"", "&quot;")
            .Replace("'", "&apos;");

        return $@"<?xml version=""1.0"" encoding=""UTF-8""?>
<speak version=""1.0"" xmlns=""http://www.w3.org/2001/10/synthesis"" xml:lang=""en-US"">
<break time=""{pauseMs}ms""/>{escapedText}
</speak>";
    }
}
