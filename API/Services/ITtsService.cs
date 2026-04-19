namespace TTS_API.Services;

public interface ITtsService
{
    byte[] Synthesize(string text, string? voice = null, int? rate = null, int? volume = null);
    IEnumerable<VoiceInfo> GetVoices();
}

public interface IAzureTtsService
{
    Task<byte[]> SynthesizeAsync(string text, string voice, string? style = null, string? rate = null, string? pitch = null);
    Task<IEnumerable<AzureVoiceInfo>> GetVoicesAsync();
    bool IsConfigured { get; }
}

public record VoiceInfo(string Name, string Gender, string Age, string Culture);

public record AzureVoiceInfo(
    string Name,
    string DisplayName,
    string Gender,
    string Locale,
    string[] Styles
);
