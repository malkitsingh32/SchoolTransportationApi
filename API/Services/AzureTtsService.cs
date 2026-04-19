using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace TTS_API.Services;

public class AzureTtsService : IAzureTtsService, IDisposable
{
    private readonly HttpClient _httpClient;
    private readonly string? _subscriptionKey;
    private readonly string? _region;
    private string? _accessToken;
    private DateTime _tokenExpiry = DateTime.MinValue;
    private bool _disposed;

    public bool IsConfigured => !string.IsNullOrEmpty(_subscriptionKey) && !string.IsNullOrEmpty(_region);

    public AzureTtsService(IConfiguration configuration)
    {
        _httpClient = new HttpClient();
        _subscriptionKey = configuration["Azure:SpeechKey"];
        _region = configuration["Azure:SpeechRegion"];
    }

    public async Task<byte[]> SynthesizeAsync(
        string text,
        string voice,
        string? style = null,
        string? rate = null,
        string? pitch = null)
    {
        if (!IsConfigured)
        {
            throw new InvalidOperationException(
                "Azure TTS not configured. Add Azure:SpeechKey and Azure:SpeechRegion to appsettings.json");
        }

        await EnsureAccessTokenAsync();

        var ssml = BuildSsml(text, voice, style, rate, pitch);

        var request = new HttpRequestMessage(HttpMethod.Post,
            $"https://{_region}.tts.speech.microsoft.com/cognitiveservices/v1");

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
        request.Headers.Add("X-Microsoft-OutputFormat", "audio-16khz-128kbitrate-mono-mp3");
        request.Headers.Add("User-Agent", "TTS-API");

        request.Content = new StringContent(ssml, Encoding.UTF8, "application/ssml+xml");

        var response = await _httpClient.SendAsync(request);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Azure TTS failed ({response.StatusCode}): {error}");
        }

        return await response.Content.ReadAsByteArrayAsync();
    }

    public async Task<IEnumerable<AzureVoiceInfo>> GetVoicesAsync()
    {
        if (!IsConfigured)
        {
            return Enumerable.Empty<AzureVoiceInfo>();
        }

        await EnsureAccessTokenAsync();

        var request = new HttpRequestMessage(HttpMethod.Get,
            $"https://{_region}.tts.speech.microsoft.com/cognitiveservices/voices/list");

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);

        var response = await _httpClient.SendAsync(request);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Failed to list voices: {response.StatusCode}");
        }

        var json = await response.Content.ReadAsStringAsync();
        var voices = JsonSerializer.Deserialize<List<AzureVoiceResponse>>(json, 
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new();

        return voices.Select(v => new AzureVoiceInfo(
            v.ShortName,
            v.DisplayName,
            v.Gender,
            v.Locale,
            v.StyleList ?? Array.Empty<string>()
        ));
    }

    private async Task EnsureAccessTokenAsync()
    {
        if (_accessToken != null && DateTime.UtcNow < _tokenExpiry)
        {
            return;
        }

        var request = new HttpRequestMessage(HttpMethod.Post,
            $"https://{_region}.api.cognitive.microsoft.com/sts/v1.0/issueToken");

        request.Headers.Add("Ocp-Apim-Subscription-Key", _subscriptionKey);

        var response = await _httpClient.SendAsync(request);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Failed to get access token: {response.StatusCode}");
        }

        _accessToken = await response.Content.ReadAsStringAsync();
        _tokenExpiry = DateTime.UtcNow.AddMinutes(9);
    }

    private static string BuildSsml(
        string text,
        string voice,
        string? style,
        string? rate,
        string? pitch)
    {
        var escapedText = text
            .Replace("&", "&amp;")
            .Replace("<", "&lt;")
            .Replace(">", "&gt;")
            .Replace("\"", "&quot;")
            .Replace("'", "&apos;");

        var lang = voice.Length >= 5 ? voice.Substring(0, 5) : "en-US";

        var sb = new StringBuilder();
        sb.AppendLine(@"<speak version=""1.0"" xmlns=""http://www.w3.org/2001/10/synthesis""");
        sb.AppendLine(@"       xmlns:mstts=""https://www.w3.org/2001/mstts""");
        sb.AppendLine($@"       xml:lang=""{lang}"">");
        sb.AppendLine($@"  <voice name=""{voice}"">");

        if (!string.IsNullOrEmpty(style))
        {
            sb.AppendLine($@"    <mstts:express-as style=""{style}"">");
        }

        if (!string.IsNullOrEmpty(rate) || !string.IsNullOrEmpty(pitch))
        {
            var prosodyAttrs = new List<string>();
            if (!string.IsNullOrEmpty(rate)) prosodyAttrs.Add($@"rate=""{rate}""");
            if (!string.IsNullOrEmpty(pitch)) prosodyAttrs.Add($@"pitch=""{pitch}""");
            sb.AppendLine($@"      <prosody {string.Join(" ", prosodyAttrs)}>");
            sb.AppendLine($"        {escapedText}");
            sb.AppendLine(@"      </prosody>");
        }
        else
        {
            sb.AppendLine($"      {escapedText}");
        }

        if (!string.IsNullOrEmpty(style))
        {
            sb.AppendLine(@"    </mstts:express-as>");
        }

        sb.AppendLine(@"  </voice>");
        sb.AppendLine(@"</speak>");

        return sb.ToString();
    }

    public void Dispose()
    {
        if (!_disposed)
        {
            _httpClient.Dispose();
            _disposed = true;
        }
        GC.SuppressFinalize(this);
    }

    private class AzureVoiceResponse
    {
        public string Name { get; set; } = "";
        public string DisplayName { get; set; } = "";
        public string ShortName { get; set; } = "";
        public string Gender { get; set; } = "";
        public string Locale { get; set; } = "";
        public string[]? StyleList { get; set; }
    }
}
