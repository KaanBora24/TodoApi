using Microsoft.AspNetCore.Mvc;

namespace TodoApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AiController : ControllerBase
{
    public record SuggestRequest(string Text);

    [HttpPost("suggest")]
    public IActionResult Suggest([FromBody] SuggestRequest req)
    {
        if (req is null || string.IsNullOrWhiteSpace(req.Text))
            return BadRequest("Text boş olamaz.");

        var items = GenerateTodos(req.Text);
        return Ok(new { items });
    }

    private static List<string> GenerateTodos(string text)
    {
        var t = text.Trim();

        // Basit “AI gibi” kurallar:
        // - Metni kısaltılmış aksiyonlara çevirir
        // - 5-8 öneri üretir
        var suggestions = new List<string>();

        // Bazı anahtar kelimeler üzerinden akıllı eklemeler
        if (ContainsAny(t, "spor", "gym", "antrenman"))
            suggestions.Add("Antrenman için 45-60 dk blok ayır");

        if (ContainsAny(t, "proje", "ödev", "backend", "api"))
            suggestions.Add("Görevleri 3 parçaya böl: planla → uygula → test et");

        if (ContainsAny(t, "ders", "çalış", "final", "sınav"))
            suggestions.Add("25 dk çalışma + 5 dk mola (Pomodoro) ile 3 tur yap");

        // Metinden cümle/virgül parçalama
        var parts = t
            .Split(new[] { '.', ',', ';', '\n' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(x => x.Trim())
            .Where(x => x.Length >= 3)
            .Take(5)
            .ToList();

        foreach (var p in parts)
        {
            // “yapacağım” gibi ekleri kırpıp aksiyona çevirme (basit)
            var action = p
                .Replace("yapacağım", "")
                .Replace("yapcam", "")
                .Replace("yapıcam", "")
                .Replace("edeceğim", "")
                .Replace("edecem", "")
                .Trim();

            if (action.Length >= 3)
                suggestions.Add($"→ {Cap(action)}");
        }

        // Genel best-practice önerileri (AI havası)
        suggestions.Add("Öncelik sırası: 1) en acil 2) en önemli 3) hızlı kazanım");
        suggestions.Add("En zor işi güne başlarken ilk 30 dk’da bitir");

        // 8’i geçmesin
        return suggestions.Distinct().Take(8).ToList();
    }

    private static bool ContainsAny(string text, params string[] words)
        => words.Any(w => text.Contains(w, StringComparison.OrdinalIgnoreCase));

    private static string Cap(string s)
        => char.ToUpper(s[0]) + s.Substring(1);
}
