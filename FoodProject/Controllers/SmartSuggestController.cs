using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductProject.Services;
[AllowAnonymous]
[Route("api/[controller]")]
public class SmartSuggestController : ControllerBase
{
    private readonly ProductSuggestService _service;

    public SmartSuggestController(ProductSuggestService service)
    {
        _service = service;
    }

    [HttpPost("ask")]
    public async Task<IActionResult> Ask([FromBody] ChatInput input)
    {
        var reply = await _service.GetSuggestionAsync(input.message);
        return Ok(new { reply });
    }

}

public class ChatInput
{
    public string message { get; set; }
}
