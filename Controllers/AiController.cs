using Microsoft.AspNetCore.Mvc;
using OpenAI.Chat;
using System.Linq;

namespace TodoApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AiController : ControllerBase
{
    private readonly ChatClient _chatClient;

    public AiController(ChatClient chatClient)
    {
        _chatClient = chatClient;
    }
    [HttpPost("motivate")]
    public async Task<IActionResult> Motivate([FromBody] AiRequest request)
    {
        var messages = new List<ChatMessage>
    {
        ChatMessage.CreateSystemMessage("Kullanıcıya 10 kelimeyi aşmadan tek bir motivasyon cümlesi yaz."),
        ChatMessage.CreateUserMessage(request.Prompt)
    };

        var result = await _chatClient.CompleteChatAsync(messages);

        var text = result.Value.Content[0].Text;

        return Ok(new
        {
            message = text
        });
    }
    public record SuggestRequest(string Text);

    [HttpPost("suggest")]
    public async Task<IActionResult> Suggest([FromBody] SuggestRequest req)
    {
        if (req is null || string.IsNullOrWhiteSpace(req.Text))
            return BadRequest("Text boş olamaz.");

        var messages = new List<ChatMessage>
        {
            new SystemChatMessage(
                "Türkçe 5-8 maddelik kısa, yapılabilir TODO listesi üret. " +
                "Sadece maddeleri yaz. Her madde yeni satırda olsun."
            ),
            new UserChatMessage(req.Text)
        };

        var result = await _chatClient.CompleteChatAsync(messages);

        var completion = result.Value;
        var text = string.Join("", completion.Content.Select(c => c.Text));

        var items = text
            .Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .Select(x => x.Trim().TrimStart('-', '•', '*', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '.', ')'))
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Take(8)
            .ToList();

        return Ok(new { items });
    }
}
