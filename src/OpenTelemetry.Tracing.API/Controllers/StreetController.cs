using Microsoft.AspNetCore.Mvc;
using OpenTelemetry.Tracing.API.Class;

namespace OpenTelemetry.Tracing.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StreetController : ControllerBase
{
    private readonly HttpClient client;
    private readonly IUserProvider userProvider;

    public StreetController(HttpClient client, IUserProvider userProvider)
    {
        this.client = client;
        this.userProvider = userProvider;
    }

    [HttpGet]
    public async Task<IEnumerable<string>> Get()
    {
        var users = userProvider.Get();
        var response = await client.GetAsync("https://www.google.com/search?q=a&oq=a");
        return new string[] {"value1", "value2"};   
    }
}
