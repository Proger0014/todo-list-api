using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Extensions.DependencyInjection;
using TodoList.DB;
using TodoList.Models.RefreshToken;
using TodoList.Models.User;

namespace IntegrationTests.Common;

public class TestWebApplicationFactory<TProgram> :
    WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Test");
    }
}
