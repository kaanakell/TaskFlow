using System.Net.Http.Json;
using Xunit;

namespace TaskFlow.Tests;

public class TaskApiTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public TaskApiTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task CreateTask_Then_GetAll_ReturnTask()
    {
        var createRequest = new
        {
            title = "Test Task",
            description = "Integration test task"
        };

        var postResponse = await _client.PostAsJsonAsync("/api/tasks", createRequest);
        postResponse.EnsureSuccessStatusCode();

        var getResponse = await _client.GetAsync("/api/tasks");
        getResponse.EnsureSuccessStatusCode();

        var tasks = await getResponse.Content.ReadAsStringAsync();
        Assert.Contains("Test Task", tasks);
    }
}