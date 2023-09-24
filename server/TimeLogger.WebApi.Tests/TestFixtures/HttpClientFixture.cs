using System;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace TimeLogger.Api.Tests.TestFixtures
{
    public class HttpClientFixture : IDisposable
    {
        public readonly HttpClient Client;
        private readonly TestServer _server;

        public HttpClientFixture()
        {
            var hostBuilder = new WebHostBuilder()
                .UseEnvironment("Testing")
                .UseStartup<Startup>();
            
            _server = new TestServer(hostBuilder);
            Client = _server.CreateClient();
        }

        public void Dispose()
        {
            // Cleanup resources after the tests
            Client.Dispose();
            _server.Dispose();
        }
    }
}